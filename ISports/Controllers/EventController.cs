using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISports.Controllers
{
    public class EventController : Controller
    {
        // GET: Evento
        public ActionResult Home()
        {
           /* if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            } */ 

            return View();
        }

        public ActionResult Manager()
        {
            /* if (Session["usuario"] == null)
             {
                 return RedirectToAction("Login", "Account");
             } */
            return View();
        }

        public ActionResult Search()
        {
            /* if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            } */
            return View();
        }

        public ActionResult Create()
        {
            /* if (Session["usuario"] == null)
             {
                 return RedirectToAction("Login", "Account");
             } */
            return View();
        }

        public ActionResult FeedEvents()
        {
            /* if (Session["usuario"] == null)
             {
                 return RedirectToAction("Login", "Account");
             } */

            return View();
            
        }

        public ActionResult UserEvents()
        {
            /* if (Session["usuario"] == null)
             {
                 return RedirectToAction("Login", "Account");
             } */

            return View();
        }

    }
}