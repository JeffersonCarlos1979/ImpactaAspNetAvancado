using Empresa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class EmrpesaDbContext : DbContext
    {
        public EmrpesaDbContext(DbContextOptions options): base(options)
        {

            Database.EnsureCreated();

            
        }

        public DbSet<Contato> Contatos { get; set; }
    }
}
