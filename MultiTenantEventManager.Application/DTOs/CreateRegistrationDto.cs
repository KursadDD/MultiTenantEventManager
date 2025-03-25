using MultiTenantEventManager.Domain.Entities;
using MultiTenantEventManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.DTOs
{
    public class CreateRegistrationDto
    {
        [MinLength(3), MaxLength(200), Required(ErrorMessage = "İsim alanı boş olamaz")]
        public string FullName { get; set; }

        [MaxLength(200), Required(ErrorMessage = "E-posta alanı boş olamaz")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string? Email { get; set; }
      
    }
}
