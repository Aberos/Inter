using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISports.Models;
using ISports.Filtro;

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