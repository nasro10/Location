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
    public class ClaimController : Controller
    {
        ServiceClaim myService = null;
        public ClaimController()
        {
            myService = new ServiceClaim();
        }
        // GET: Claim
        public ActionResult Index()
        {
            var res = myService.GetMany().ToList();
            return View(res);
        }

        // GET: Claim/Details/5
        public ActionResult Details(int id)
        {
            Claims c = myService.GetById(id);
            if (ModelState.IsValid)
            {
                c.state = ClaimState.Open;
                myService.Update(c);
                myService.Commit();
            }
            return View(c);
        }


        // GET: Claim/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Claim/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Claims c)
        {
            if (ModelState.IsValid)
            {
                c.ClaimDate = DateTime.Now;
                c.UserN = User.Identity.Name;
                c.state = ClaimState.New;
                myService.Add(c);
                myService.Commit();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        //[HttpGet]
        //public JsonResult GetNotifications()
        //{
        //    List<DataSubmit> lstDataSubmit = new List<DataSubmit>();

        //    /// Should update from DB
        //    ///
        //    ///e.g. Generating Notification manually
        //    var No = 10;
        //    while (No != 0)
        //    {
        //        lstDataSubmit.Add(new DataSubmit() { Notification = "This is dynamic notification..." + No, LastUpdated = DateTime.Now.ToString("ss") + " seconds ago..." });
        //        No--;
        //    }
        //    return Json(lstDataSubmit, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TreatClaim(Claims claim)
        {
            Claims c = myService.GetById(claim.ClaimID);
            if (ModelState.IsValid)
            {
                c.state = ClaimState.Closed;
                myService.Update(c);
                myService.Commit();
            }
            return View(claim);
        }

    }
}
