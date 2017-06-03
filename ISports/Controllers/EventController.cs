using ISports.Filtro;
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
        [UsuarioFiltro]
        public ActionResult Home()
        {

            int idEvento = int.Parse(Request.QueryString["EventoID"]);
            Evento e = new Evento();
            using (EventoModel model = new EventoModel())
            {
                e = model.Read(idEvento);
            }

            using (EventoModel model = new EventoModel())
            {
                ViewBag.listSubs = model.InscritosEvento(idEvento);
                ViewBag.Subscribed = model.UserSubscribed(idEvento, (Session["usuario"] as Usuario).Id_usuario);

                if(ViewBag.Subscribed == true)
                {
                    ViewBag.SubStatus = model.getSubscribeStatus((Session["usuario"] as Usuario).Id_usuario, idEvento);
                }
                else
                {
                    ViewBag.SubStatus = 0;
                }
            }

            return View(e);
        }

        [UsuarioFiltro]
        public ActionResult Manager()
        {

            int idEvento = int.Parse(Request.QueryString["EventoID"]);
            Evento e = new Evento();
            using (EventoModel model = new EventoModel())
            {
                e = model.Read(idEvento);
                ViewBag.listSubs = model.InscritosEvento(e.Id_Evento);
            }
            
            return View(e);
        }

        [UsuarioFiltro]
        public ActionResult Search()
        {
            return View();
        }

        [UsuarioFiltro]
        public ActionResult Create()
        {

            using (CidadeModel cm = new CidadeModel())
            {
                ViewBag.Cidades = cm.Cidades();
            }

            using (UfModel uf = new UfModel())
            {
                ViewBag.Estados = uf.Ufs();
            }

            using (EsporteModel em = new EsporteModel())
            {
                ViewBag.Esportes = em.Esportes();
            }

         return View();

        }

        [HttpPost]
        [UsuarioFiltro]
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

        [UsuarioFiltro]
        public ActionResult FeedEvents()
        {

            using (EventoModel model = new EventoModel())
            {
                ViewBag.Feed = model.FeedEvents((Session["usuario"] as Usuario).Id_usuario);
            }
                
            return View();
            
        }

        [UsuarioFiltro]
        public ActionResult UserEvents()
        {

            using (EventoModel model = new EventoModel())
            {
                ViewBag.UserEvents = model.MyEvents((Session["usuario"] as Usuario).Id_usuario);
            }
                

            return View();
        }

        [UsuarioFiltro]
        public ActionResult Subscribe(int idEvento)
        {
            int idUser = (Session["usuario"] as Usuario).Id_usuario;
            using (EventoModel model = new EventoModel())
            {
                model.InscreverEvento(idEvento, idUser);
            }
                
            return RedirectToAction("Home", "Event", new { EventoID = idEvento });
        }

        [UsuarioFiltro]
        public ActionResult Unsubscribe(int idEvento)
        {
            int idUser = (Session["usuario"] as Usuario).Id_usuario;
            using (EventoModel model = new EventoModel())
            {
                model.DesinscreverEvento(idEvento, idUser);
            }

            return RedirectToAction("Home", "Event", new { EventoID = idEvento });
        }

    }
}