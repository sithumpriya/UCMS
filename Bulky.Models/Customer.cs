using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }
    }
}
