using ISports.Models;
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
            
            if (Session["usuario"] == null)
             {
                 return RedirectToAction("Login", "Account");
             }

            CidadeModel cm = new CidadeModel();
            UfModel uf = new UfModel();
            EsporteModel em = new EsporteModel();
            ViewBag.Esportes = em.Esportes();
            ViewBag.Estados = uf.Ufs();
            ViewBag.Cidades = cm.Cidades();

         return View();

        }

        [HttpPost]
        public ActionResult Create(Evento e)
        {
            if(ModelState.IsValid)
            {
                using (EventoModel model = new EventoModel())
                {
                    e.Organizador.Id_usuario = (Session["usuario"] as Usuario).Id_usuario;
                    model.Create(e);
                }
                return RedirectToAction("UserEvents", "Event");
            }
            else
            {
                return View(e);
            }
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