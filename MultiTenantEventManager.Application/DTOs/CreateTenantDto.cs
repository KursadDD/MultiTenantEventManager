using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.DTOs
{
    public class CreateTenantDto
    {
        [MinLength(3), MaxLength(50), Required(ErrorMessage = "İsim alanı boş olamaz")]
        public string Name { get; set; }
    }
}
