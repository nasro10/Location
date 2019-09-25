using Location.Domain.Entities;
using Location.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    [AllowAnonymous]
    public class RatingsController : Controller
    {
        ServiceRating myService = null;
        ServiceCar serviceCar = null;
        public RatingsController()
        {
            myService = new ServiceRating();
            serviceCar = new ServiceCar();
        }
        // GET: Ratings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetRating(int carId, int number,string description)
        {
            Rate rating = new Rate();
            rating.Number = number;
            rating.CarId = carId;
            rating.Description = description;
            var db = new LocationVoiture.Data.LocationContext();
            var CurrentUser = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            rating.UserId = CurrentUser.UserID;

            myService.Add(rating);
            myService.Commit();


            return RedirectToAction("Index", "Cars", new { id = carId });
        }

        public PartialViewResult RatingsControl(int carId)
        {
            Car car = serviceCar.GetById(carId);

            return PartialView("_RatingsControl", car);
        }
    }
}