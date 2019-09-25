using Location.Domain.Entities;
using Location.Service;
using Location.Service.Interfaces;
using Location.Service.Service;
using LocationVoiture.Data;
using LocationVoiture.Helpers;
using LocationVoiture.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LocationVoiture.Areas.Protal.Controllers
{
    [Authorize(Roles = "Client")]
    public class CarsController : Controller
    {
        ServiceRating serviceRating = null;
        ServiceCar myService = null;
        ServiceSubCategory serviceSubCategory = null;
        ServiceCarModel serviceCarModel = null;
        ServiceCreationYear serviceCreation = null;
        ServiceTypeCar serviceType = null;
        ServiceBooking serviceBooking = null;
        ServiceImage serviceImage = null;
        ServiceUser serviceUser = null;


        public object CarId { get; private set; }

        public CarsController()
        {
            myService = new ServiceCar();
            serviceRating = new ServiceRating();
            serviceSubCategory = new ServiceSubCategory();
            serviceCarModel = new ServiceCarModel();
            serviceCreation = new ServiceCreationYear();
            serviceType = new ServiceTypeCar();
            serviceBooking = new ServiceBooking();
            serviceImage = new ServiceImage();
            serviceUser = new ServiceUser();
            
        }

      
        [AllowAnonymous]
        // GET: Car
        public ActionResult Index(string searchString, string sortOrder)
        {
            IEnumerable<Car> products = myService.GetMany().Where(w => w.Available == true && w.Active == true).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.CarModel.Name.Contains(searchString));
            }
          
            switch (sortOrder)
            {
                case "bypricePerhour":
                    products = products.OrderByDescending(s => s.PricePerHour);
                    break;
                case "bypricePerday":
                    products = products.OrderByDescending(s => s.PricePerDay);
                    break;
                case "bypricePermonth":
                    products = products.OrderByDescending(s => s.PricePerMonth);
                    break;
                case "byname":
                    products = products.OrderBy(s => s.CarModel.Name);
                    break;

                default:
                    products = products.OrderBy(s => s.MinPriceForRent);
                    break;
            }
            return View(products);
        }

        public ActionResult MyCar()
        {
            var db = new LocationContext();
            var CurrentUser = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var res = myService.GetMany().Where(x => x.UserId == CurrentUser.UserID).ToList();
            return View(res);
        }
        
        public ActionResult CarDetail(int id)
        {
            Car car = myService.GetById(id);
            ViewBag.note = serviceRating.avgRate(car);
            ViewBag.note1 = serviceRating.avgRateUser(car.User);
            return View(car);
        }
        [AllowAnonymous]
        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            Car car = myService.GetById(id);
           
            Session["CarId"] = id;
            ViewBag.note = serviceRating.avgRate(car);
            ViewBag.note1 = serviceRating.avgRateUser(car.User);
            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            var res = serviceCarModel.GetMany().ToList();
            var sub = serviceSubCategory.GetMany().ToList();
            var year = serviceCreation.GetMany().ToList();
            var type = serviceType.GetMany().ToList();

            ViewBag.TypeId = new SelectList(type, "TypeId", "Name");
            ViewBag.CreationYearId = new SelectList(year, "CreationYearId", "Name");
            ViewBag.SubCategoryId = new SelectList(sub, "SubCategoryId", "Name");
            ViewBag.CarModelId = new SelectList(res, "CarModelId", "Name");
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                
                var db = new LocationVoiture.Data.LocationContext();
                var CurrentUser = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
                if (CurrentUser.IsActive == true)
                {
                    car.UserId = CurrentUser.UserID;
                    car.Available = true;
                    car.Active = true;
                    myService.Add(car);
                    myService.Commit();
                    serviceImage.AddPictureToCar(car.CarID, files);

                    ViewBag.Result = "Car was added successfully!";
                }
                else
                {
                    ViewBag.Result = "You have to activate you account if you want to add a car";
                }
               
            }
            var res = serviceCarModel.GetMany().ToList();
            var sub = serviceSubCategory.GetMany().ToList();
            var year = serviceCreation.GetMany().ToList();
            var type = serviceType.GetMany().ToList();
            ViewBag.TypeId = new SelectList(type, "TypeId", "Name");
            ViewBag.CreationYearId = new SelectList(year, "CreationYearId", "Name");
            ViewBag.SubCategoryId = new SelectList(sub, "SubCategoryId", "Name");
            ViewBag.CarModelId = new SelectList(res, "CarModelId", "Name");
            return View(car);
        }

        // GET: Car/Edit/5
        public ActionResult EditCar(int id)
        {
            var res = serviceCarModel.GetMany().ToList();
            var sub = serviceSubCategory.GetMany().ToList();
            var year = serviceCreation.GetMany().ToList();
            var type = serviceType.GetMany().ToList();

            ViewBag.TypeId = new SelectList(type, "TypeId", "Name");
            ViewBag.CreationYearId = new SelectList(year, "CreationYearId", "Name");
            ViewBag.SubCategoryId = new SelectList(sub, "SubCategoryId", "Name");
            ViewBag.CarModelId = new SelectList(res, "CarModelId", "Name");
            Car car = myService.GetById(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car car, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Car c = myService.GetById(car.CarID);
                c.SerialNumber = car.SerialNumber;
                c.PricePerMonth = car.PricePerMonth;
                c.Note = car.Note;
                c.MinPriceForRent = car.MinPriceForRent;
                c.PricePerDay = car.PricePerDay;
                c.PricePerHour = car.PricePerHour;
                c.seatNumber = car.seatNumber;
                c.Fuel = car.Fuel;
                c.Gear = car.Gear;
                c.TypeId = car.TypeId;
                c.CarModelId = car.CarModelId;
                c.SubCategoryId = car.SubCategoryId;
                c.CreationYearId = car.CreationYearId;

                myService.Update(c);
                myService.Commit();
                serviceImage.AddPictureToCar(car.CarID, files);
                return RedirectToAction("Index");


            }
            return View(car);
        }

        // GET: Car/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Car car = myService.GetById(id);
            myService.Delete(car);
            myService.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #region Booking


        public ActionResult CreateBooking()
        {
            List<SelectListItem> rentType = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Per Hour", Value = "1"
        },
        new SelectListItem {
            Text = "Per Day", Value = "2"
        },
        new SelectListItem {
            Text = "Per Month", Value = "3"
        },
            };
            ViewBag.RentType = rentType;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                
                var db = new LocationVoiture.Data.LocationContext();
                var CurrentUser = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
                
                var carId = (int)Session["CarId"];
                booking.UserId= CurrentUser.UserID;
                booking.CarId = carId;

                
                
                
                    booking.bookingState = BookingState.Sent;
                    booking.BookingRequestDate = DateTime.Now;
                    serviceBooking.Add(booking);
                    serviceBooking.Commit();
                    Car car = myService.GetById(carId);

                    // booking.Car.User.Token;
                    NotificationHelper.SendFCMNotificationWithBadgeAndroid(car.User.Token, "Booking Request", "New Rental Request", 1, 0, "application/json");
                    return RedirectToAction("Index");
               

               
                }
            return View(booking);
           
        }
        #endregion

        [HttpPost]
        public JsonResult SaveToken()
        {
            var CurrentUser = serviceUser.GetMany().Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            CurrentUser.Token = "e1zNav3W_sY:APA91bFjIF-Uew8p9ZO8TLFNZqpY_S_XAm0MC16snP03GIHKoRFBC0U3h_KaUT0I38NKDB7oFe8lT0f7Hcg_pGAoZV_EcTEZHvbKKAu3TGAgDtRQnnymiRHEIZD9Z4ocJzomT8-BnxtZ";
            serviceUser.Update(CurrentUser);
            serviceUser.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }


        #region Rating
        [HttpPost]
        public JsonResult Rate()
        {
            int value = int.Parse(Request.Form["value"]);
            int CarID = int.Parse(Request.Form["id"]);
            Rate rate = new Rate { CarId = CarID, rate1 = value };
            var db = new LocationVoiture.Data.LocationContext();
            var CurrentUser = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            rate.UserId = CurrentUser.UserID;
            serviceRating.addRate(rate);
            serviceRating.commit();
            Car car = myService.GetById(CarID); 
            double s = serviceRating.avgRate(car);

            return Json(s);
        }


        [HttpPost]
        public JsonResult RateUser()
        {
            int val = int.Parse(Request.Form["valueu"]);
            int UserID = int.Parse(Request.Form["id"]);
            Rate rate = new Rate { UserId = UserID, Number = val };
       
            
            serviceRating.addRate(rate);
            serviceRating.commit();
            User user = serviceUser.GetById(UserID);
            double s = serviceRating.avgRateUser(user);

            return Json(s);
        }


        #endregion



        //#region payment
        //public ActionResult PaymentWithPaypal()
        //{
        //    //getting the apiContext as earlier
        //    APIContext apiContext = Configuration.GetAPIContext();

        //    try
        //    {
        //        string payerId = Request.Params["PayerID"];

        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            //this section will be executed first because PayerID doesn't exist
        //            //it is returned by the create function call of the payment class

        //            // Creating a payment
        //            // baseURL is the url on which paypal sendsback the data.
        //            // So we have provided URL of this controller only
        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
        //                        "/Cars/PaymentWithPayPal?";

        //            //guid we are generating for storing the paymentID received in session
        //            //after calling the create function and it is used in the payment execution

        //            var guid = Convert.ToString((new Random()).Next(100000));

        //            //CreatePayment function gives us the payment approval url
        //            //on which payer is redirected for paypal account payment

        //            var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

        //            //get links returned from paypal in response to Create function call

        //            var links = createdPayment.links.GetEnumerator();

        //            string paypalRedirectUrl = null;

        //            while (links.MoveNext())
        //            {
        //                Links lnk = links.Current;

        //                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    //saving the payapalredirect URL to which user will be redirected for payment
        //                    paypalRedirectUrl = lnk.href;
        //                }
        //            }

        //            // saving the paymentID in the key guid
        //            Session.Add(guid, createdPayment.id);

        //            return Redirect(paypalRedirectUrl);
        //        }
        //        else
        //        {
        //            // This section is executed when we have received all the payments parameters

        //            // from the previous call to the function Create

        //            // Executing a payment

        //            var guid = Request.Params["guid"];

        //            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

        //            if (executedPayment.state.ToLower() != "approved")
        //            {
        //                return View("FailureView");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PaypalLogger.Log("Error" + ex.Message);
        //        return View("FailureView");
        //    }

        //    return View("SuccessView");
        //}
        //private Payment payment;

        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{
        //    var listItems = new ItemList() { items = new List<Item>() };

        //    listItems.items.Add(new Item()
        //    {
        //        name = "Nasro",
        //        currency = "USD",
        //        price = "5",
        //        quantity = "1",
        //        sku = "sku"
        //    });
        //    var payer = new Payer() { payment_method = "paypal" };
        //    // Configure Redirect Urls here with RedirectUrls object
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl,
        //        return_url = redirectUrl
        //    };

        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "1",
        //        subtotal = "5"
        //    };

        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = "7", // Total must be equal to sum of shipping, tax and subtotal.
        //        details = details
        //    };

        //    var transactionList = new List<Transaction>();

        //    transactionList.Add(new Transaction()
        //    {
        //        description = "My Items",
        //        invoice_number = "your invoice number",
        //        amount = amount,
        //        item_list = listItems
        //    });

        //    payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };

        //    // Create a payment using a APIContext
        //    return payment.Create(apiContext);

        //}


        //// Create ExecutePayment method
        //private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution() { payer_id = payerId };
        //    payment = new Payment() { id = paymentId };
        //    return payment.Execute(apiContext, paymentExecution);
        //}



        //#endregion



        //public ActionResult PaymentWithPaypal(string Cancel = null)
        //{
        //    //getting the apiContext  
        //    APIContext apiContext = Configuration.GetAPIContext();
        //    try
        //    {
        //        //A resource representing a Payer that funds a payment Payment Method as paypal  
        //        //Payer Id will be returned when payment proceeds or click to pay  
        //        string payerId = Request.Params["PayerID"];
        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            //this section will be executed first because PayerID doesn't exist  
        //            //it is returned by the create function call of the payment class  
        //            // Creating a payment  
        //            // baseURL is the url on which paypal sendsback the data.  
        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cars/PaymentWithPayPal?";
        //            //here we are generating guid for storing the paymentID received in session  
        //            //which will be used in the payment execution  
        //            var guid = Convert.ToString((new Random()).Next(100000));
        //            //CreatePayment function gives us the payment approval url  
        //            //on which payer is redirected for paypal account payment  
        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
        //            //get links returned from paypal in response to Create function call  
        //            var links = createdPayment.links.GetEnumerator();
        //            string paypalRedirectUrl = null;
        //            while (links.MoveNext())
        //            {
        //                Links lnk = links.Current;
        //                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    //saving the payapalredirect URL to which user will be redirected for payment  
        //                    paypalRedirectUrl = lnk.href;
        //                }
        //            }
        //            // saving the paymentID in the key guid  
        //            Session.Add(guid, createdPayment.id);
        //            return Redirect(paypalRedirectUrl);
        //        }
        //        else
        //        {
        //            // This function exectues after receving all parameters for the payment  
        //            var guid = Request.Params["guid"];
        //            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
        //            //If executed payment failed then we will show payment failure message to user  
        //            if (executedPayment.state.ToLower() != "approved")
        //            {
        //                return View("FailureView");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("FailureView");
        //    }
        //    //on successful payment, show success page to user.  
        //    return View("SuccessView");
        //}
        //private PayPal.Api.Payment payment;
        //private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution()
        //    {
        //        payer_id = payerId
        //    };
        //    this.payment = new Payment()
        //    {
        //        id = paymentId
        //    };
        //    return this.payment.Execute(apiContext, paymentExecution);
        //}
        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{
        //    //create itemlist and add item objects to it  
        //    var itemList = new ItemList()
        //    {
        //        items = new List<Item>()
        //    };
        //    //Adding Item Details like name, currency, price etc  
        //    itemList.items.Add(new Item()
        //    {
        //        name = "Item Name comes here",
        //        currency = "USD",
        //        price = "1",
        //        quantity = "1",
        //        sku = "sku"
        //    });
        //    var payer = new Payer()
        //    {
        //        payment_method = "paypal"
        //    };
        //    // Configure Redirect Urls here with RedirectUrls object  
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl + "&Cancel=true",
        //        return_url = redirectUrl
        //    };
        //    // Adding Tax, shipping and Subtotal details  
        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "1",
        //        subtotal = "1"
        //    };
        //    //Final amount with details  
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
        //        details = details
        //    };
        //    var transactionList = new List<Transaction>();
        //    // Adding description about the transaction  
        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Transaction description",
        //        invoice_number = "your generated invoice number", //Generate an Invoice No  
        //        amount = amount,
        //        item_list = itemList
        //    });
        //    this.payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };
        //    // Create a payment using a APIContext  
        //    return this.payment.Create(apiContext);
        //}
    }
}
