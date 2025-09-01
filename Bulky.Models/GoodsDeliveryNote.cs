using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class GoodsDeliveryNote
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [DisplayName("GDN Date")]
        public DateTime GDNDate { get; set; }

        public string? Remark { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}
