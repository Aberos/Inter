using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISports.Controllers
{
    public class EventHomeController : Controller
    {
        // GET: EventHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexManager()
        {
            return View();
        }
    }
}