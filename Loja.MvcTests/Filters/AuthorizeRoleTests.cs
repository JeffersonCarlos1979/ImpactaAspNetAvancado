using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loja.Mvc.Models;

namespace Loja.Mvc.Filters.Tests
{
    [TestClass()]
    public class AuthorizeRoleTests
    {
        [TestMethod()]
        public void AuthorizeRoleTest()
        {
            //Arranje/Act
            var authorizeRole = new AuthorizeRole(Perfil.Administrador, Perfil.Comprador);

            //Assert
            Assert.IsTrue(authorizeRole.Roles.Contains("Administrador"));
            Assert.IsTrue(authorizeRole.Roles.Contains("Comprador"));

        }
    }
}