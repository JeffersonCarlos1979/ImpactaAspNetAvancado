using Loja.Mvc.Filters;
using Loja.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult DefinirLinguagem(string linguagem)
        {
            Response.Cookies["LinguagemSelecionada"].Value = linguagem;

            return Redirect(Request.UrlReferrer.ToString());

            /*Para visualizar os cook no navegado, é digitar F12, netwok, selecionar a action (DefinirLinguagem?linguagem=es)
             E aí dá pra visualizar o cookie*/
        }

        [AuthorizeRole(Perfil.Administrador)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //if (!User.IsInRole(Perfil.Leiloeiro.ToString()))
            if (!((ClaimsIdentity)User.Identity).HasClaim("Leilao", "Cadastrar"))
            {
                return RedirectToAction("Login","Account",new { area = ""});
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }


        
    }
}