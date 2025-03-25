using MultiTenantEventManager.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.DTOs
{
    public class UpdateUserDto
    {
        [MinLength(3), MaxLength(50), Required(ErrorMessage = "İsim alanı boş olamaz")]
        public string Name { get; set; }

        [MaxLength(100), Required(ErrorMessage = "E-posta alanı boş olamaz")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}:;,.?<>])[A-Za-z\d!@#$%^&*()_+{}:;,.?<>]{6,}$",
        ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string Password { get; set; }
    }
}
