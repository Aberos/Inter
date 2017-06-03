using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISports.Models;
using ISports.Filtro;
using System.Web.Hosting;

namespace ISports.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario c)
        {
            if(ModelState.IsValid)
            {
                using (UsuarioModel model = new UsuarioModel())
                {
                    model.Create(c);
                }
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Mensagem = "Erro";
                return View(c);
            }
        }
        [HttpPost]
        public ActionResult Login(Usuario l)
        {
            using (UsuarioModel model = new UsuarioModel())
            {
                Usuario user = model.Read(l.Email, l.Senha);

                if(user == null)
                {
                    ViewBag.Erro = "E-mail ou Senha inválidas";
                }
                else
                {
                    Session["usuario"] = user;
                    ViewBag.Picture = (Session["usuario"] as Usuario).Foto_Perfil;
                    return RedirectToAction("FeedEvents", "Event");
                }
            }
            return View();
        }
        [UsuarioFiltro]
        [HttpPost]
        public ActionResult EditImage(Usuario u)
        {
            int iduser = ((Usuario)Session["usuario"]).Id_usuario;
            HttpPostedFileBase arquivo = Request.Files[0];                                         

            using (System.Drawing.Image pic = System.Drawing.Image.FromStream(arquivo.InputStream)) 
            {
                /*if (pic.Height != 256 && pic.Width != 256)
                {
  
                    return RedirectToAction("Edit");
                }
                else*/ if (arquivo.ContentType != "image/png" && arquivo.ContentType != "image/jpeg" && arquivo.ContentType != "image/jpg")           
                {

                    return RedirectToAction("Edit");
                }
                else if (arquivo.ContentLength > 2097152)                                             
                {

                    return RedirectToAction("Edit");
                }
            }

            if (Request.Files.Count > 0)                                                          
            {

                if (arquivo.ContentLength > 0)                                               
                {
                    string img = "/Picutres/" + iduser.ToString() + System.IO.Path.GetExtension(arquivo.FileName);
                    string path = HostingEnvironment.ApplicationPhysicalPath;                       

                    string caminho = path + "\\Pictures\\" + iduser.ToString() + System.IO.Path.GetExtension(arquivo.FileName);
                    arquivo.SaveAs(caminho);
                    u.Foto_Perfil = img;
                }
            }
            using (UsuarioModel model = new UsuarioModel())
            {
                model.UpdateImage(u);                              
            }
            return RedirectToAction("Edit");
        }


        [UsuarioFiltro]
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Recover()
        {
            return View();
        }
    }
}