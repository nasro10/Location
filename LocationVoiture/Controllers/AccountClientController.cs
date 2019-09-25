using Location.Domain.Entities;
using Location.Service;
using LocationVoiture.Data;
using LocationVoiture.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace LocationVoiture.Areas.Protal.Controllers
{
    [AllowAnonymous]
    public class AccountClientController : Controller
    {
        ServiceUser myService = null;
        public AccountClientController()
        {
            myService = new ServiceUser();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            int nbr = myService.LoginCheck(u);
            
            if (nbr == 0)
            {
                ViewBag.Message = "Invalid User";
                return View();
            }
            else
            {
               FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index", "Cars");

            }

        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User u)
        {
            if (myService.checkUserName(u).Count() < 1)
            {
                if (ModelState.IsValid)
                {
                    u.SignUpDate = DateTime.Now;
                    u.IsActive = false;
                    u.Role = "Client";

                    myService.Add(u);
                    myService.Commit();
                    BuildEmailTemplate(u.UserID);
                    return RedirectToAction("Index", "HomeClient");
                }
            }
            else
            {
                ViewBag.Result = "this User Name is already Taken, try another one!";
            }
          
            return View(u);
        }

        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }

        public JsonResult RegisterConfirm(int regId)
        {
            User Data = myService.GetMany().Where(x => x.UserID == regId).FirstOrDefault();
            Data.IsActive = true;
            myService.Commit();
            var msg = "Your Email is verified!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = myService.GetMany().Where(x => x.UserID == regID).FirstOrDefault();
            //confirmation Link for send with email
            var url = "http://localhost:51155/" + "AccountClient/Confirm?regId=" + regID;
            body = body.Replace("@Viewbag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account is successfully Created", body, regInfo.Email);
        }
        //build email for sending (subject,body,from,to)
        public void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "nasreddine1410@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            client.Credentials = new System.Net.NetworkCredential("nasreddine1410@gmail.com", "nasroisthebest");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Client")]
        public ActionResult UserProfile(User u, HttpPostedFileBase upload, HttpPostedFileBase workrep, HttpPostedFileBase workp
            , HttpPostedFileBase cinp)
        {
            User user = myService.GetById(u.UserID);
            if (ModelState.IsValid)
            {



                user.Name = u.Name;
                user.UserName = u.UserName;
                user.LastName = u.LastName;
                user.SignUpDate = u.SignUpDate;
                user.Email = u.Email;
                user.CIN = u.CIN;
                user.phone = u.phone;



                user.Picture = MyHelper.UploadFile(upload, 3);



                user.BirthDate = u.BirthDate;
                user.Role = u.Role;
                user.CreditCard = u.CreditCard;
                user.Gender = u.Gender;
                user.IsActive = u.IsActive;
                myService.Update(user);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(u);
        }
    }
}
