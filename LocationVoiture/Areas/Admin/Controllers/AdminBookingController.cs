using Location.Domain.Entities;
using Location.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminBookingController : Controller
    {
        ServiceBooking myService = null;
        ServiceInvoice serviceInvoice = null;
        public AdminBookingController()
        {
            myService = new ServiceBooking();
            serviceInvoice = new ServiceInvoice();
        }
        // GET: Admin/AdminBooking
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }


        // GET: Admin/AdminBooking/Details/5
        public ActionResult Details(int id)
        {
            Booking booking = myService.GetById(id);
            return View(booking);
        }

        // GET: Admin/AdminBooking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminBooking/Create
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

        // GET: Admin/AdminBooking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/AdminBooking/Edit/5
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

        // GET: Admin/AdminBooking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/AdminBooking/Delete/5
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
