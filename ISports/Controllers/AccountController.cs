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
                TempData["Created"] = "Account Created";
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
            try
            {
                using (UsuarioModel model = new UsuarioModel())
                {
                    Usuario user = model.Read(l.Email, l.Senha);

                    if (user == null)
                    {
                        ViewBag.Erro = "E-mail ou Senha inválidas";
                        return View();
                    }
                    else
                    {
                        Session["usuario"] = user;
                        ViewBag.Picture = (Session["usuario"] as Usuario).Foto_Perfil;
                        return RedirectToAction("FeedEvents", "Event");
                    }
                }
            }catch
            {
                ViewBag.Erro = "E-mail ou Senha inválidas";
                return View();
            }       
        }
        [UsuarioFiltro]
        [HttpPost]
        public ActionResult EditImage(FormCollection form)
        {
            int iduser = ((Usuario)Session["usuario"]).Id_usuario;
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
                        TempData["errorProfile"] = "Check the photo format";
                        return RedirectToAction("Edit", "Account");
                    }
                    else if (arquivo.ContentLength > 2097152)
                    {
                        TempData["errorProfile"] = "Very large picture";
                        return RedirectToAction("Edit", "Account");
                    }
                }

                if (Request.Files.Count > 0)
                {

                    if (arquivo.ContentLength > 0)
                    {
                        DateTime today = DateTime.Now;
                        string img = "/Pictures/User/" + iduser.ToString() + today.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(arquivo.FileName);
                        string path = HostingEnvironment.ApplicationPhysicalPath;

                        string caminho = path + "\\Pictures\\User\\" + iduser.ToString() + today.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(arquivo.FileName);
                        arquivo.SaveAs(caminho);
                        Foto_Perfil = img;
                    }
                }
                using (UsuarioModel model = new UsuarioModel())
                {
                    model.UpdateImage(iduser, Foto_Perfil);
                }
                TempData["successProfile"] = "Picutere Changed";
                return RedirectToAction("Edit", "Account");
            }
            else {
                TempData["errorProfile"] = "Erro Changed Picutere";
                return RedirectToAction("Edit", "Account");
            }       
       }


        [UsuarioFiltro]
        public ActionResult Edit()
        {
            Usuario user = new Usuario();
            using (UsuarioModel model = new UsuarioModel())
            {
                user = model.ReadId((Session["usuario"] as Usuario).Id_usuario);


                Session["usuario"] = user;

            }
            return View(user);
        }

        [UsuarioFiltro]
        [HttpPost]
        public ActionResult Edit(Usuario u)
        {
            Usuario user = new Usuario();
            try
            {
                using (UsuarioModel model = new UsuarioModel())
                {
                    u.Id_usuario = (Session["usuario"] as Usuario).Id_usuario;
                    model.EditUser(u);
                }

                using (UsuarioModel model = new UsuarioModel())
                {
                    user = model.ReadId((Session["usuario"] as Usuario).Id_usuario);
                }

                TempData["successProfile"] = "Profile Changed";

            }
            catch
            {
                using (UsuarioModel model = new UsuarioModel())
                {
                    user = model.ReadId((Session["usuario"] as Usuario).Id_usuario);
                }
                TempData["errorProfile"] = "Erro Changed Profile";
            }
            return View(user);
        }

        public ActionResult Recover()
        {
            return View();
        }

        public ActionResult ChangePassword(FormCollection form)
        {
            string senha = form["password"];
            string nova = form["newpassword"];
            try
            {

                using (UsuarioModel model = new UsuarioModel())
                {
                    int idUser = (Session["usuario"] as Usuario).Id_usuario;
                    model.ChangePassowrd(idUser, senha, nova);
                }
                TempData["successProfile"] = "Password Changed";
                return RedirectToAction("Edit", "Account");
            }
            catch
            {
                TempData["errorProfile"] = "Erro Changed Password";
                return RedirectToAction("Edit", "Account");
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}