using Location.Domain.Entities;
using Location.Service;
using LocationVoiture.Areas.Admin.Models;
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
    public class SubCategoryController : Controller
    { 
        //LocationContext db = new LocationContext();
        ServiceSubCategory myService = null;
        ServiceCategory myServicecat = null;
        public SubCategoryController()
        {
            myService = new ServiceSubCategory();
            myServicecat = new ServiceCategory();
        }
        // GET: Admin/SubCategory
        public ActionResult Index()
        {
            var res = myService.GetMany();
            List<SubCategoryViewModel> liste = new List<SubCategoryViewModel>();
            foreach (var item in res)
            {
                liste.Add(
                    new SubCategoryViewModel
                    {
                        SubCategoryID = item.SubCategoryID,
                        Name = item.Name,
                        Picture = item.Picture,
                        Subscription = item.Subscription,
                        Active = item.Active,
                        CategoryName = myServicecat.GetById(item.CategoryId).Name
                    });
            }

            return View(liste);


        }

        // GET: SubCategory/Details/5
        public ActionResult Details(int id)
        {
            SubCategory s = myService.GetById(id);
            return View(s);

        }

        // GET: SubCategory/Create
        public ActionResult Create()
        {
            var res = new SubCategoryViewModel();
            res.Categories = myServicecat.GetMany().ToSelectListItems();
            return View(res);

        }

        // POST: SubCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategoryViewModel svm, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                SubCategory s = new SubCategory()
                {

                    Name = svm.Name,
                    Picture = svm.Picture= MyHelper.UploadFile(upload, 3),
                    Subscription = svm.Subscription,
                    Active = svm.Active,
                    CategoryId = svm.CategoryID

                };

               
                myService.Add(s);
                myService.Commit();
                return RedirectToAction("Index");

            }

            return View(svm);
        }

        // GET: SubCategory/Edit/5
        public ActionResult Edit(int id)
        {
            SubCategory s = myService.GetById(id);
            return View(s);
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategory sub, HttpPostedFileBase upload)
        {
            SubCategory s = myService.GetById(sub.SubCategoryID);
            if (ModelState.IsValid)
            {

       
                s.Name = sub.Name;
                sub.Picture = s.Picture = MyHelper.UploadFile(upload, 3);
                s.Category = sub.Category;
                s.cars = sub.cars;
                s.Subscription = sub.Subscription;
                s.Active = sub.Active;

                myService.Update(s);
                myService.Commit();
                return RedirectToAction("Index");

            }
            return View(sub);

        }

        // GET: SubCategory/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            SubCategory s = myService.GetById(id);
            myService.Delete(s);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}