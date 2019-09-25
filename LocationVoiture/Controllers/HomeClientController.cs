using Location.Service;
using Location.Service.Service;
using LocationVoiture.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Location.Domain.Entities;

namespace LocationVoiture.Controllers
{
    [AllowAnonymous]
    public class HomeClientController : Controller
    {
        ServiceCategory myService = null;
        ServiceCountry serviceCountry = null;
        ServiceState serviceState = null;
        ServiceCategory serviceCategory = null;
        ServiceSubCategory serviceSubCategory = null;
        ServiceCar serviceCar = null;
        ServiceRating serviceRating = null;
        ServiceUser serviceUser = null;

        public HomeClientController()
        {
            myService = new ServiceCategory();
            serviceCountry = new ServiceCountry();
            serviceState = new ServiceState();
            serviceCategory = new ServiceCategory();
            serviceSubCategory = new ServiceSubCategory();
            serviceCar = new ServiceCar();
            serviceRating = new ServiceRating();
            serviceUser = new ServiceUser();
        }
        public ActionResult Index()
        {
            
            return View(myService.GetMany().ToList());
        }

        public ActionResult tres()
        {

            var country = serviceCountry.GetMany().ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select Country--", Value = "0" });

            foreach (var m in country)
            {


                li.Add(new SelectListItem { Text = m.CountryName, Value = m.CountryID.ToString() });
                ViewBag.country = li;

            }
            return View();
        }

        public JsonResult getstate(int id)
        {
            var states = serviceState.GetMany().Where(x => x.CountryId == id).ToList();
            List<SelectListItem> listates = new List<SelectListItem>();

            listates.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.StateName, Value = x.StateID.ToString() });

                }



            }


            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }



        void SendMessage(string message)
        {
            GlobalHost
           .ConnectionManager
           .GetHubContext<NotificationHub>().Clients.All(); 
        }


        public ActionResult Test()
        {
            ViewBag.Message = "We sent you an email in order to veify your Account";
            return View();
        }

        public ActionResult ActiveAcc()
        {
            ViewBag.Message = "Your account should be active if you want to book a car or Add you own Car";
            return View();
        }

        public ActionResult Book()
        {
            ViewBag.Message = "Sorry, You've already sent a Rental Request for this car!";
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Categories()
        {
            var res = serviceCategory.GetMany().Where(x => x.Active == true).ToList();
            return View(res);
        }

        public ActionResult subs(int id)
        {
            var res = serviceSubCategory.GetMany().Where(x => x.CategoryId == id).ToList();
            return View(res);
        }

        public ActionResult CarCat(int id, int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var res1 = serviceCar.GetMany().Where(x => x.SubCategoryId == id&&x.Active==true).ToList();
          foreach(var c in res1)
            {
                ViewBag.note = serviceRating.avgRate(c);
            }
            return View(res1.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Chat()
        {
            var currentUser= serviceUser.GetMany().Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            return View(currentUser);
        }
    }
}