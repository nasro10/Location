using Location.Domain.Entities;
using Location.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class CarModelController : Controller
    {
        // GET: Admin/CarModel
        ServiceCarModel myService = null;
        public CarModelController()
        {
            myService = new ServiceCarModel();
        }
        // GET: CarModel
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: CarModel/Details/5
        public ActionResult Details(int id)
        {
            CarModel c = myService.GetById(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // GET: CarModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarModel c)
        {
            if (ModelState.IsValid)
            {
                myService.Add(c);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        // GET: CarModel/Edit/5
        public ActionResult Edit(int id)
        {
            CarModel cr = myService.GetById(id);
            if (cr == null)
            {
                return HttpNotFound();
            }
            return View(cr);
        }

        // POST: CarModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarModel car)
        {
            if (ModelState.IsValid)
            {
                CarModel c = myService.GetById(car.CarModelID);
                c.Name = car.Name;

                c.Active = car.Active;
                c.Cars = car.Cars;
                myService.Update(c);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: CarModel/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            CarModel s = myService.GetById(id);
            myService.Delete(s);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}
