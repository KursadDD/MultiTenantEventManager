using MultiTenantEventManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.DTOs
{
    public class UpdateEventDto
    {
        [MinLength(3), MaxLength(100), Required]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime Date { get; set; }
        public string? Location { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Capacity en az 1 olmalıdır.")]
        public int Capacity { get; set; }
        public EventStatusType Status { get; set; }
    }
}
