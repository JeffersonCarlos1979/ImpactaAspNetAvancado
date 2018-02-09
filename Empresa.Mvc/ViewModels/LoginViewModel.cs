using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Empresa.Mvc.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [DataType(DataType.Password)]
        public String Senha { get; set; }
    }
}
