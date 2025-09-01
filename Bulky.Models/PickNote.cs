using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class PickNote
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [DisplayName("Pick Note Date")]
        public DateTime PNDate { get; set; }

        public string? Remark { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }
    }
}
