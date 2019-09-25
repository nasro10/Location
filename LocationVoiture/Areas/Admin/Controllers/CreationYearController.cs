using Location.Domain.Entities;
using Location.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CreationYearController : Controller
    {
        ServiceCreationYear myService = null;
        public CreationYearController()
        {
            myService = new ServiceCreationYear();
        }
        // GET: Admin/CreationYear
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: CreationYear/Details/5
        public ActionResult Details(int id)
        {
            CreationYear c = myService.GetById(id);
            return View(c);
        }

        // GET: CreationYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreationYear/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreationYear cr)
        {
            myService.Add(cr);
            myService.Commit();
            return RedirectToAction("Index");
        }

        // GET: CreationYear/Edit/5
        public ActionResult Edit(int id)
        {
            CreationYear cr = myService.GetById(id);
            return View(cr);
        }

        // POST: CreationYear/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreationYear cr)
        {
            CreationYear c = myService.GetById(cr.CreationYearID);
            c.Name = cr.Name;
            c.Active = cr.Active;
            c.Cars = cr.Cars;
            myService.Update(c);
            myService.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            CreationYear creationYear = myService.GetById(id);
            myService.Delete(creationYear);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}