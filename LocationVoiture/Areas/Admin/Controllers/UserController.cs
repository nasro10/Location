using Location.Domain.Entities;
using Location.Service;
using LocationVoiture.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserController : Controller
    {
        ServiceUser myService = null;
        public UserController()
        {
            myService = new ServiceUser();
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            var res = myService.GetMany().Where(x => x.Role == "Client").ToList();
            return View(res);
        }



        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User u)
        {
            if (ModelState.IsValid)
            {
                u.SignUpDate = DateTime.Now;
                u.Role = "Client";
                u.IsActive = true;
                myService.Add(u);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(u);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User u = myService.GetById(id);
            return View(u);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User u, HttpPostedFileBase upload, HttpPostedFileBase workrep, HttpPostedFileBase workp
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


        [HttpPost]
        public JsonResult Delete(int id)
        {
            User user = myService.GetById(id);
            myService.Delete(user);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult BanUser(int id)
        {
            try
            {
                User Data = myService.GetMany().Where(x => x.UserID == id).FirstOrDefault();
                Data.IsActive = false;
                Data.ConfirmPassword = Data.Password;
                myService.Update(Data);
                myService.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActiveClient(int id)
        {
            try
            {
                User Data = myService.GetMany().Where(x => x.UserID == id).FirstOrDefault();
                Data.IsActive = true;
                Data.ConfirmPassword = Data.Password;
                myService.Update(Data);
                myService.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}