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
    public class CategoryController : Controller
    {
        ServiceCategory myService = null;
        public CategoryController()
        {
            myService = new ServiceCategory();
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            Category c = myService.GetById(id);
            return View(c);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category c, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                c.Picture = MyHelper.UploadFile(upload, 3);
                myService.Add(c);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            Category c = myService.GetById(id);
            return View(c);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category category, HttpPostedFileBase upload)
        {
            Category c = myService.GetById(category.CategoryID);
            if (ModelState.IsValid)
            {
              
            
                c.Name = category.Name;
                c.Picture = category.Picture = c.Picture = MyHelper.UploadFile(upload, 3);
                c.Description = category.Description;
                c.subCategories = category.subCategories;
                c.Active = category.Active;

                myService.Update(c);
                myService.Commit();
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Category/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Category s = myService.GetById(id);
            myService.Delete(s);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}
