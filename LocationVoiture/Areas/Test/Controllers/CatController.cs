using Location.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Test.Controllers
{
    [AllowAnonymous]
    public class CatController : Controller
    {
        ServiceCategory myService = null;
        public CatController()
        {
            myService = new ServiceCategory();
        }
        // GET: Test/Cat
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: Test/Cat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Cat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Cat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Cat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Cat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Cat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Cat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
