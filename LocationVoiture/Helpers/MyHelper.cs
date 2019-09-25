using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace LocationVoiture.Helpers
{
    public static class MyHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Category> Categories)
        {
            return
                Categories.OrderBy(s => s.Name)
                      .Select(a =>
                          new SelectListItem
                          {
                              //Selected = (loc.CIN == selectedId),
                              Text = a.Name ,
                              Value = a.CategoryID.ToString()
                          });
        }


        public static IEnumerable<SelectListItem> ToSelectListItems(
           this IEnumerable<Country> Countries)
        {
            return
                Countries.OrderBy(s => s.CountryName)
                      .Select(a =>
                          new SelectListItem
                          {
                              //Selected = (loc.CIN == selectedId),
                              Text = a.CountryName,
                              Value = a.CountryID.ToString()
                          });
        }

        public static string UploadFile(HttpPostedFileBase file, int count)
        {
            string filename = count + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                   + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()
                   + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".png";
             string path = Path.Combine(HostingEnvironment.MapPath("~/Uploads"), filename);

            file.SaveAs(path);
            return filename;
        }
    }

}