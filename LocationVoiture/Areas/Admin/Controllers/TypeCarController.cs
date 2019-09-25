using Location.Domain.Entities;
using Location.Service;
using LocationVoiture.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TypeCarController : Controller
    {
        ServiceTypeCar myService = null;
        public TypeCarController()
        {
            myService = new ServiceTypeCar();
        }
        // GET: Admin/TypeCar
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: TypeCar/Details/5
        public ActionResult Details(int id)
        {
            TypeCar c = myService.GetById(id);
            return View(c);
        }

        // GET: TypeCar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeCar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeCar car, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
               car.Picture= MyHelper.UploadFile(upload, 3);
                myService.Add(car);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: TypeCar/Edit/5
        public ActionResult Edit(int id)
        {
            TypeCar car = myService.GetById(id);
            return View(car);
        }

        // POST: TypeCar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TypeCar car, HttpPostedFileBase upload)
        {
            TypeCar t = myService.GetById(car.TypeID);
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                car.Picture = upload.FileName;
                t.Name = car.Name;
                t.Picture = car.Picture;
                t.Active = car.Active;
                t.Cars = car.Cars;
                myService.Update(t);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(t);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            TypeCar type = myService.GetById(id);
            myService.Delete(type);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}