using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorios.SqlServer;
using Empresa.Dominio;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        private EmrpesaDbContext _db;// = new EmrpesaDbContext();
        private IDataProtector _protectorProvider;

        //Nesse construtor entra o conceito de injeção de dependência
        public ContatosController(EmrpesaDbContext db, IDataProtectionProvider 
            protectionProvider, IConfiguration configuracao)
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(
                configuracao.GetSection("ChaveCriptografia").Value);

        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View(_db.Contatos.OrderBy(c=> c.Nome).ToList());
        }


        [Authorize(Roles = "Admin, Corretor")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Corretor")]
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            contato.Senha = _protectorProvider.Protect(contato.Senha);
            _db.Contatos.Add(contato);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
