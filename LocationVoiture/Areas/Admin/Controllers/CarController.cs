using Location.Domain.Entities;
using Location.Service;
using Location.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CarController : Controller
    {
        ServiceCar myService = null;
        ServiceSubCategory serviceSubCategory = null;
        ServiceCarModel serviceCarModel = null;
        ServiceCreationYear serviceCreation = null;
        ServiceTypeCar serviceType = null;
        ServiceImage serviceImage = null;
        ServiceRating serviceRating = null;

        public CarController()
        {
            myService = new ServiceCar();
            serviceSubCategory = new ServiceSubCategory();
            serviceCarModel = new ServiceCarModel();
            serviceCreation = new ServiceCreationYear();
            serviceType = new ServiceTypeCar();
            serviceImage = new ServiceImage();
            serviceRating = new ServiceRating();
        }
        // GET: Admin/Car
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            Car car = myService.GetById(id);
            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            var res = serviceCarModel.GetMany().ToList();
            var sub = serviceSubCategory.GetMany().ToList();
            var year = serviceCreation.GetMany().ToList();
            var type = serviceType.GetMany().ToList();

            ViewBag.TypeId = new SelectList(type, "TypeId", "Name");
            ViewBag.CreationYearId = new SelectList(year, "CreationYearId", "Name");
            ViewBag.SubCategoryId = new SelectList(sub, "SubCategoryId", "Name");
            ViewBag.CarModelId = new SelectList(res, "CarModelId", "Name");
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                myService.Add(car);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public ActionResult EditCar(int id)
        {
            var res = serviceCarModel.GetMany().ToList();
            var sub = serviceSubCategory.GetMany().ToList();
            var year = serviceCreation.GetMany().ToList();
            var type = serviceType.GetMany().ToList();

            ViewBag.TypeId = new SelectList(type, "TypeId", "Name");
            ViewBag.CreationYearId = new SelectList(year, "CreationYearId", "Name");
            ViewBag.SubCategoryId = new SelectList(sub, "SubCategoryId", "Name");
            ViewBag.CarModelId = new SelectList(res, "CarModelId", "Name");
            Car car = myService.GetById(id);
            ViewBag.note = serviceRating.avgRate(car);
            ViewBag.note1 = serviceRating.avgRateUser(car.User);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car car)
        {
            if (ModelState.IsValid)
            { 
                Car c = myService.GetById(car.CarID);
                c.SerialNumber = car.SerialNumber;
                c.PricePerMonth = car.PricePerMonth;
                c.Note = car.Note;
                c.MinPriceForRent = car.MinPriceForRent;
                c.PricePerDay = car.PricePerDay;
                c.PricePerHour = car.PricePerHour;
                c.seatNumber = car.seatNumber;
                c.Fuel = car.Fuel;
                c.Gear = car.Gear;
                c.TypeId = car.TypeId;
                c.CarModelId = car.CarModelId;
                c.SubCategoryId = car.SubCategoryId;
                c.CreationYearId = car.CreationYearId;

                myService.Update(c);
                myService.Commit();
             //   serviceImage.AddPictureToCar(car.CarID, files);
                return RedirectToAction("Index");


            }
            return View(car);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Car car = myService.GetById(id);
            myService.Delete(car);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BanCar(int id)
        {
            try
            {
                Car Data = myService.GetMany().Where(x => x.CarID == id).FirstOrDefault();
                Data.Active = false;
              //  Data.ConfirmPassword = Data.Password;
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
        public JsonResult ActivateCar(int id)
        {
            try
            {
                Car Data = myService.GetMany().Where(x => x.CarID == id).FirstOrDefault();
                Data.Active = true;
                //  Data.ConfirmPassword = Data.Password;
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