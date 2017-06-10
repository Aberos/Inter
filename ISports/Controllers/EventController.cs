using ISports.Filtro;
using ISports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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
            string estado = Request.QueryString["estado"];
            ViewBag.UF = "-- Select UF --";
            Evento e = new Evento();

            using (UfModel uf = new UfModel())
            {
                ViewBag.Estados = uf.Ufs();
            }

            using (EsporteModel em = new EsporteModel())
            {
                ViewBag.Esportes = em.Esportes();
            }

            using (EventoModel model = new EventoModel())
            {
                e = model.Read(idEvento);
                ViewBag.listSubs = model.InscritosEvento(e.Id_Evento);
            }

            using (CidadeModel cm = new CidadeModel())
            {
                if (estado.Length > 0)
                {
                    ViewBag.UF = estado;
                    ViewBag.Cidades = cm.Cidades(estado);
                    e.Local.Cidade.Uf.Sigla = estado;
                    e.Local.Cidade.codigo = 0;
                    e.Local.Cidade.Nome = null;

                }
                else
                {
                    ViewBag.Cidades = cm.Cidades(e.Local.Cidade.Uf.Sigla);
                }
                        
            }

            return View(e);
        }

        [UsuarioFiltro]
        [HttpPost]
        public ActionResult Manager(Evento e, int id)
        {
            int idEvento = id;
            if (ModelState.IsValid)
            {
                using (EventoModel model = new EventoModel())
                {
                    e.Id_Evento = idEvento;
                    model.AlterarEvento(e);
                }
                return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
            }
            else
            {
                ViewBag.Mensagem = "Erro";
                return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
            }
            
        }

        [UsuarioFiltro]
        public ActionResult Search()
        {
            return View();
        }

        [UsuarioFiltro]
        public ActionResult Create(string estado, FormCollection form)
        {
            ViewBag.UF = "-- Select UF --";
            using (UfModel uf = new UfModel())
            {
                ViewBag.Estados = uf.Ufs();
            }

            using (CidadeModel cm = new CidadeModel())
            {

                if (estado != null)
                {
                    ViewBag.UF = estado;
                    ViewBag.Cidades = cm.Cidades(estado);
                }
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

        [UsuarioFiltro]
        [HttpPost]
        public ActionResult EditImageEvent(FormCollection form, int id)
        {
            int idEvento = id;
            HttpPostedFileBase arquivo = Request.Files[0];
            if (ModelState.IsValid)
            {
                string Foto_Perfil = null;
                using (System.Drawing.Image pic = System.Drawing.Image.FromStream(arquivo.InputStream))
                {
                    /*if (pic.Height != 256 && pic.Width != 256)
                    {

                        return RedirectToAction("Edit");
                    }
                    else*/
                    if (arquivo.ContentType != "image/png" && arquivo.ContentType != "image/jpeg" && arquivo.ContentType != "image/jpg")
                    {

                        return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
                    }
                    else if (arquivo.ContentLength > 2097152)
                    {

                        return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
                    }
                }

                if (Request.Files.Count > 0)
                {

                    if (arquivo.ContentLength > 0)
                    {
                        string img = "/Pictures/Event/" + idEvento.ToString() + System.IO.Path.GetExtension(arquivo.FileName);
                        string path = HostingEnvironment.ApplicationPhysicalPath;

                        string caminho = path + "\\Pictures\\Event\\" + idEvento.ToString() + System.IO.Path.GetExtension(arquivo.FileName);
                        arquivo.SaveAs(caminho);
                        Foto_Perfil = img;
                    }
                }
                using (EventoModel model = new EventoModel())
                {
                    model.UpdateImage(idEvento, Foto_Perfil);
                }

                return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
            }
            else
            {

                return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
            }
        }

    }
}