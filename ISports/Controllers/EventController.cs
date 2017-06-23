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
                ViewBag.Noticias = model.NoticiasEvento(idEvento);
                ViewBag.NotaUser = model.getNotaUserEvento(idEvento, (Session["usuario"] as Usuario).Id_usuario);

                if (ViewBag.Subscribed == true)
                {
                    ViewBag.SubStatus = model.getSubscribeStatus((Session["usuario"] as Usuario).Id_usuario, idEvento);
                }
                else
                {
                    ViewBag.SubStatus = 0;
                }

            }

            using (EventoModel model = new EventoModel())
            {
                if (model.isAdmin(idEvento, (Session["usuario"] as Usuario).Id_usuario))
                {
                    return RedirectToAction("Manager", "Event", new { EventoID = idEvento });
                }
                else
                {
                    return View(e);
                }

            }


            
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

            using (CidadeModel cm = new CidadeModel())
            {
                ViewBag.Cidades = cm.Cidades();
            }

            using (EsporteModel em = new EsporteModel())
            {
                ViewBag.Esportes = em.Esportes();
            }

            using (EventoModel model = new EventoModel())
            {
                if (model.isAdmin(idEvento, (Session["usuario"] as Usuario).Id_usuario))
                {
                    e = model.Read(idEvento);
                    ViewBag.listSubs = model.InscritosEvento(e.Id_Evento);
                    ViewBag.Noticias = model.NoticiasEvento(e.Id_Evento);
                    return View(e);
                }
                else
                {
                    return RedirectToAction("FeedEvents", "Event");
                }

            }
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
        public ActionResult Search(string estado)
        {
            ViewBag.UF = "UF";
            using (UfModel uf = new UfModel())
            {
                ViewBag.Estados = uf.Ufs();
            }

            using (CidadeModel cm = new CidadeModel())
            {
                ViewBag.Cidades = cm.Cidades();

            }
            using (EsporteModel em = new EsporteModel())
            {
                ViewBag.Esportes = em.Esportes();
            }

            return View();
        }
        
        [UsuarioFiltro]
        public ActionResult ResultSearch()
        {
            try
            {
                string estado = Request.QueryString["estado"];
                int cidade = int.Parse(Request.QueryString["cidade"]);
                int esporte = int.Parse(Request.QueryString["esporte"]);
                string nome = Request.QueryString["nome"];

                using (EventoModel ev = new EventoModel())
                {
                    if (nome == null)
                    {
                        nome = "";
                    }
                    ViewBag.ListEventos = ev.Search(cidade, estado, esporte, nome);
                }
                return View();
            }catch
            {
               return RedirectToAction("Search", "Event");
            }
            
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
                 ViewBag.Cidades = cm.Cidades();
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
            try
            {
                if (ModelState.IsValid)
                {
                    using (EventoModel model = new EventoModel())
                    {
                        e.Organizador.Id_usuario = (Session["usuario"] as Usuario).Id_usuario;
                        model.Create(e);
                    }                  
                    TempData["sucessoCriar"] = "Success";
                    return RedirectToAction("UserEvents", "Event");
                }
                else
                {
                    TempData["erroCriar"] = "Error";
                    return RedirectToAction("UserEvents", "Event");
                }
            }
            catch
            {
                TempData["erroCriar"] = "Erro";
                return RedirectToAction("UserEvents", "Event");
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
                        DateTime today = DateTime.Now;
                        string img = "/Pictures/Event/" + idEvento.ToString() + today.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(arquivo.FileName);
                        string path = HostingEnvironment.ApplicationPhysicalPath;

                        string caminho = path + "\\Pictures\\Event\\" + idEvento.ToString() + today.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(arquivo.FileName);
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

        public ActionResult AcceptUserEvent(int IdEvent, int IdUser)
        {
            using (EventoModel model = new EventoModel())
            {
                model.ChangeStatusSubscribe(IdEvent, IdUser, 1);
            }
            return RedirectToAction("Manager", "Event", new { EventoID = IdEvent });
        }

        public ActionResult RemoveUserEvent(int IdEvent, int IdUser)
        {
            using (EventoModel model = new EventoModel())
            {
                model.ChangeStatusSubscribe(IdEvent, IdUser, 3);
            }
            return RedirectToAction("Manager", "Event", new { EventoID = IdEvent });
        }

        public ActionResult CadNoticia(int IdEvent, string msg)
        {
            using (EventoModel model = new EventoModel())
            {
                if(model.isAdmin(IdEvent, (Session["usuario"] as Usuario).Id_usuario))
                {
                    model.CadNoticia(IdEvent, msg);
                    return RedirectToAction("Manager", "Event", new { EventoID = IdEvent });
                }
                else
                {
                    return RedirectToAction("FeedEvents", "Event");
                }
                
            }
        }

        public ActionResult DeleteEvento(int IdEvent)
        {
            using (EventoModel model = new EventoModel())
            {
                if (model.isAdmin(IdEvent, (Session["usuario"] as Usuario).Id_usuario))
                {
                    model.DeleteEvento(IdEvent);
                    model.removeInscritoEvento(IdEvent);
                    return RedirectToAction("UserEvents", "Event");
                }
                else
                {
                    return RedirectToAction("UserEvents", "Event");
                }
            }
        }

        public ActionResult AvaliarEvento(int IdEvent, int Nota, int idOrg)
        {
            try
            {
                using (EventoModel model = new EventoModel())
                {
                      model.AvaliarEvento(IdEvent, (Session["usuario"] as Usuario).Id_usuario, Nota);
                }

                using (OrganizadorModel model = new OrganizadorModel())
                {
                    model.UpdateQualificacaoOrg(idOrg);
                }
                return RedirectToAction("Home", "Event", new { EventoID = IdEvent });
            }
            catch
            {
                return RedirectToAction("Home", "Event", new { EventoID = IdEvent });
            }
        }
    }
}