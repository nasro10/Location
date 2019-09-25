using Location.Domain.Entities;
using Location.Service;
using Location.Service.Service;
using LocationVoiture.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class StatsController : Controller
    {
        ServiceUser serviceUser = null;
        ServiceCar serviceCar = null;
        ServiceCreationYear myService = null; 

        public StatsController()
        {
            serviceUser = new ServiceUser();
            serviceCar = new ServiceCar();
            myService = new ServiceCreationYear();
        }
        // GET: Admin/Stats
        public ActionResult Index()
        {
            List<CreationYear> fls = myService.GetMany().ToList();
            List<dynamic> models = new List<dynamic>();
            foreach (var item in fls)
            {
                dynamic x = new
                {
                    label = item.Name,
                    value = item.Cars.Count()
                };

                models.Add(x);
            }

            return View(models);
        }
        //Line Chart
        public ActionResult GetData()
        {
            LocationContext context = new LocationContext();

            var query = context.Bookings.Include("User")
                   .GroupBy(p => p.BookingRequestDate.Month)
                   .Select(g => new { name = g.Key, count = g.Count() }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewChart()
        {
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee", System.Type.GetType("System.String"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Employee"] = "Sam";
            dr["Credit"] = 123;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Alex";
            dr["Credit"] = 456;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Michael";
            dr["Credit"] = 587;
            dt.Rows.Add(dr);
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        //pie Chart
        public ActionResult GetUsers()
        {
            int active = serviceUser.NbrActiveUsers();
            int inActive = serviceUser.NbrInActiveUsers();
            Ratio obj = new Ratio();
            obj.Active = active;
            obj.InActive = inActive;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int Active { get; set; }
            public int InActive { get; set; }
        }


        //Chart
            public ActionResult CharterColumn()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in myService.GetMany() select c);
            results.ToList().ForEach(rs => xValue.Add(rs.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Cars.Count()));

            new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                .AddTitle("Chart For Cars [Column Chart]")
                .AddSeries("Default", chartType: "column", xValue: xValue, yValues: yValue)
                .Write("bmp");



            return null;
        }


        public ActionResult UsersCars()
        {

            List<User> fls = serviceUser.GetMany().ToList();
            List<dynamic> models = new List<dynamic>();
            foreach (var item in fls)
            {
                dynamic x = new
                {
                    label = item.UserName,
                    value = item.Cars.Count()
                };

                models.Add(x);
            }

            return View(models);
        }

        public ActionResult UsersState()
        {

            List<User> fls = serviceUser.GetMany().ToList();
            int usersnbr = fls.Count();
            List<dynamic> models = new List<dynamic>();
            foreach (var item in fls)
            {
                string etat = "Active";
                if (item.IsActive == false)
                {
                    etat = "Banned User";
                }
                else
                {
                    etat = "Active User";
                }



                dynamic x = new
                {
                    label = etat,
                    value = usersnbr
                };

                models.Add(x);

            }

            return View(models);
        }

        public ActionResult CarPerYear()
        {

            List<CreationYear> fls = myService.GetMany().ToList();
            List<dynamic> models = new List<dynamic>();
            foreach (var item in fls)
            {
                dynamic x = new
                {
                    label = item.Name,
                    value = item.Cars.Count()
                };

                models.Add(x);
            }

            return View(models);
        }

    }
}