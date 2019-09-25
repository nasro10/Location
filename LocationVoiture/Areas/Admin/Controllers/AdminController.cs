using Location.Domain.Entities;
using Location.Service;
using Location.Service.Service;
using LocationVoiture.Helpers;
using LocationVoiture.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        ServiceUser myService = null;
        ServiceCountry serviceCountry = null;
        ServiceState serviceState = null;

        public AdminController()
        {
            myService = new ServiceUser();
            serviceState = new ServiceState();
            serviceCountry = new ServiceCountry();
        }

        #region Gestion Admin
        //GestionAdmin



        // GET: Admin
        public ActionResult ListAdmin()
        {
            var res = myService.ListAdmin();
            return View(res);
        }

        // GET: Admin/Create
        public ActionResult CreateAdmin()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(User u)
        {
            if (ModelState.IsValid)
            {
                u.SignUpDate = DateTime.Now;
                u.Role = "Admin";
                u.IsActive = true;
                myService.Add(u);
                myService.Commit();
                return RedirectToAction("ListAdmin");
            }

            return View(u);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            User u = myService.GetById(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User u, HttpPostedFileBase upload)
        {
            
            User user = myService.GetById(u.UserID);
            if (ModelState.IsValid)
            {
                user.Name = u.Name;
                user.UserName = u.UserName;
                user.LastName = u.LastName;
                user.Picture = MyHelper.UploadFile(upload, 3);
                user.Email = u.Email;
                user.CIN = u.CIN;

                
                user.IsActive = u.IsActive;


                myService.Commit();
                return RedirectToAction("ListAdmin");
            }
            return View(u);
        }
        // GET: Admin/Delete/5
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
                User Data = myService.selectedUser(id);
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

        #endregion

        #region Country
        // GET: Countries
        public ActionResult Countries()
        {
            var co = serviceCountry.GetMany().ToList();
            return View(co);
        }

        // GET: Country/Create
        public ActionResult CreateCountry()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCountry(Country cr)
        {
            serviceCountry.Add(cr);
            serviceCountry.Commit();
            return RedirectToAction("Countries");
        }

        // GET: EditCountry/Edit/5
        public ActionResult EditCountry(int id)
        {
            Country c = serviceCountry.GetById(id);
            return View(c);
        }

        // POST: EditCountry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCountry(Country country)
        {
            Country c = serviceCountry.GetById(country.CountryID);
            if (ModelState.IsValid)
            {

                c.CountryName = country.CountryName;

                serviceCountry.Update(c);
                serviceCountry.Commit();
                return RedirectToAction("Countries");

            }
            return View(country);

        }


        [HttpPost]
        public JsonResult DeleteCountry(int id)
        {
            Country country = serviceCountry.GetById(id);
            serviceCountry.Delete(country);
            serviceCountry.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region State
        public ActionResult States()
        {
            var s = serviceState.GetMany().ToList();
            List<StateViewModel> liste = new List<StateViewModel>();
            foreach (var item in s)
            {
                liste.Add(
                    new StateViewModel
                    {
                        StateID = item.StateID,
                        StateName = item.StateName,

                        Country = serviceCountry.GetById(item.CountryId).CountryName
                    });
            }

            return View(liste);
        }

        // GET: State/Create
        public ActionResult CreateState()
        {
            var res = new StateViewModel();
            res.Countries = serviceCountry.GetMany().ToSelectListItems();         
            return View(res);
        }

        // POST: State/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateState(StateViewModel s)
        {
            if (ModelState.IsValid)
            {
                State state = new State()
                {
                    StateName = s.StateName,
                    CountryId = s.CountryID
                };


                serviceState.Add(state);
                serviceState.Commit();
                return RedirectToAction("States");
            }
            s.Countries = serviceCountry.GetMany().ToSelectListItems();
            return View(s);
        }


        // GET: SubCategory/Edit/5
        public ActionResult EditState(int id)
        {
            State s = serviceState.GetById(id);
            return View(s);
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditState(State state)
        {
            State s = serviceState.GetById(state.StateID);
            if (ModelState.IsValid)
            {

                s.Country = state.Country;
                s.StateName = state.StateName;

                serviceState.Update(s);
                serviceState.Commit();
                return RedirectToAction("States");

            }
            return View(state);

        }


        [HttpPost]
        public JsonResult DeleteState(int id)
        {
            State state = serviceState.GetById(id);
            serviceState.Delete(state);
            serviceState.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
