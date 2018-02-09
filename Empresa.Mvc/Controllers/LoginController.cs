using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Mvc.ViewModels;
using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class LoginController : Controller
    {

        private EmrpesaDbContext _db;// = new EmrpesaDbContext();
        private IDataProtector _protectorProvider;
        private string _tipoAutenticacao;

        public LoginController(EmrpesaDbContext db, IDataProtectionProvider
            protectionProvider, IConfiguration configuracao)
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(
                configuracao.GetSection("ChaveCriptografia").Value);
            _tipoAutenticacao = configuracao.GetSection("TipoAutenticacao").Value;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {
            //viewModel.Senha= _protectorProvider.Protect(viewModel.Senha);
            //viewModel.Senha=="" || viewModel.Email == ""
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var contato = _db.Contatos.Where(c => c.Email == viewModel.Email &&
            _protectorProvider.Unprotect(c.Senha) == viewModel.Senha).SingleOrDefault();

            if (contato == null)
            {
                ModelState.AddModelError("", "Usuário ou senha incorretos");
                return View(viewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,contato.Nome),
                new Claim(ClaimTypes.Email,contato.Email),
                //new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"Vendedor"),
                new Claim("Contato","Criar"),

            };

            var identidade = new ClaimsIdentity(claims, _tipoAutenticacao);
            var principal = new ClaimsPrincipal(identidade);

            HttpContext.Authentication.SignInAsync(_tipoAutenticacao, principal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(_tipoAutenticacao);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

    }
}
