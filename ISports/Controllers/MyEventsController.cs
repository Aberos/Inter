using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISports.Controllers
{
    public class MyEventsController : Controller
    {
        // GET: MyEvents
        public ActionResult Index()
        {
            return View();
        }
    }
}