using Location.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    public class InvoiceController : Controller
    {
        ServiceInvoice myService = null;
        public InvoiceController()
        {
            myService = new ServiceInvoice();
        }
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
    }
}