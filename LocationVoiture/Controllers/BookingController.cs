using Location.Domain.Entities;
using Location.Service;
using Location.Service.Service;
using LocationVoiture.Data;
using LocationVoiture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    [Authorize(Roles = "Client")]
    public class BookingController : Controller
    {
        ServiceBooking myService = null;
        ServiceCar ServiceCar = null;
        ServiceUser ServiceClient = null;

        public BookingController()
        {
            myService = new ServiceBooking();
            ServiceCar = new ServiceCar();
            ServiceClient = new ServiceUser();
        }

        // GET: Booking
        public ActionResult RequestOnMyCars()
        {
            List<Booking> requests = new List<Booking>() ;
            var CurrentUser=   ServiceClient.GetMany().Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            try
            {
               var res = myService.GetMany().ToList();
                requests = res.Where(x => x.Car.UserId == CurrentUser.UserID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return View(requests);
        }

        public ActionResult Detail(int id)
        {
            Booking book = myService.GetById(id);
            return View(book);
        }

        public ActionResult Opened()
        {
            List<Booking> requests = new List<Booking>();
            var CurrentUser = ServiceClient.GetMany().Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            try
            {
                var res = myService.GetMany().Where(x => x.bookingState == BookingState.Sent|| x.bookingState == BookingState.Approved||
                x.bookingState == BookingState.DeliveredToClient).ToList();
                requests = res.Where(x => x.Car.UserId == CurrentUser.UserID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(requests);
           
        }

        public ActionResult Finished()
        {
            List<Booking> requests = new List<Booking>();
            var CurrentUser = ServiceClient.GetMany().Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            try
            {
                var res = myService.GetMany().Where(x => x.bookingState == BookingState.ConfirmedByOwner|| 
                x.bookingState == BookingState.Refused ||
                x.bookingState == BookingState.FinishedbyClient).ToList();
                requests = res.Where(x => x.Car.UserId == CurrentUser.UserID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(requests);
        }

        [HttpPost]
        public JsonResult AcceptRentRequest(int id)
        {
            try
            {
                Booking book = myService.GetMany().Where(x => x.BookingID == id).FirstOrDefault();
                
                book.bookingState = BookingState.Approved;
                book.BookingApproveDate = DateTime.Now;
                book.Car.Available = false;
                myService.Update(book);
                myService.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RefuseRentRequest(int id)
        {
            try
            {
                Booking book = myService.GetMany().Where(x => x.BookingID == id).FirstOrDefault();

                book.bookingState = BookingState.Refused;
                book.BookingRefuseDate = DateTime.Now;
                book.Car.Available = true;
                myService.Update(book);
                myService.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ConfirmByClient(int id)
        {
            try
            {
                Booking book = myService.GetMany().Where(x => x.BookingID == id).FirstOrDefault();

                book.bookingState = BookingState.ConfirmedByClient;
                book.BookingConfirmedByClientDate = DateTime.Now;
                
                myService.Update(book);
                myService.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FinishedByClient(int id)
        {
            try
            {
                Booking book = myService.GetMany().Where(x => x.BookingID == id).FirstOrDefault();

                book.bookingState = BookingState.FinishedbyClient;
                book.BookingFinishedbyClientDate = DateTime.Now;
                book.ActivationCode = "1256";
                myService.Update(book);
                myService.Commit();

                var mail = new MailMessage();
                var logiInfo = new NetworkCredential("nasreddine1410@gmail.com", "nasroisthebest");
                mail.From = new MailAddress("Zedney@gmail.com");
                mail.To.Add(new MailAddress(book.Car.User.Email));
                mail.Subject = "Confirmation Code";
                mail.IsBodyHtml = true;
                string body = 
                    
                    
                     "Dear Sir/Madame,"+ "<br>"+
       " This is the Activation Code for your Car: 1256" + " <b>";
                mail.Body = body;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = logiInfo;
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConfirmedByOwner(int id)
        {
            try
            {
                Booking book = myService.GetMany().Where(x => x.BookingID == id).FirstOrDefault();

                book.bookingState = BookingState.ConfirmedByOwner;
                book.BookingConfirmedByOwnerDate = DateTime.Now;
                book.Car.Available = true;
                
                if (book.ActivationCode.Equals("1256"))
                {
                    myService.Update(book);
                    myService.Commit();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult DeliverToClient (int id)
        {
            Booking book = myService.GetMany().Where(x => x.BookingID == id).FirstOrDefault();
      
            try
            {

                book.bookingState = BookingState.DeliveredToClient;
                book.BookingDeliveredToClientDate = DateTime.Now;

                myService.Update(book);
                myService.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
            
        }




        public ActionResult MyApplications()
        {
            List<Booking> requests = new List<Booking>();
            var CurrentUser = ServiceClient.GetMany().Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            try
            {
                var res = myService.GetMany().ToList();
                requests = res.Where(x => x.User.UserID == CurrentUser.UserID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(requests);

        }
    }
}