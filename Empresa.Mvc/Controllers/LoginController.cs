using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Mvc.ViewModels;
using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class LoginController : Controller
    {

        private EmrpesaDbContext _db;// = new EmrpesaDbContext();
        private IDataProtector _protectorProvider;

        public LoginController(EmrpesaDbContext db, IDataProtectionProvider
            protectionProvider, IConfiguration configuracao)
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(
                configuracao.GetSection("ChaveCriptografia").Value);
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
            var contato = _db.Contatos.Where(c => c.Email == viewModel.Email &&
            _protectorProvider.Unprotect(c.Senha) == viewModel.Senha).SingleOrDefault();

            if (contato==null)
            {
                ModelState.AddModelError("", "Usuário ou senha incorretos");
                return View(viewModel);
            }

            return RedirectToAction("Index","Home");
        }

    }
}
