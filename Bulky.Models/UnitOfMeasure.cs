using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WMS.Models
{
    public class UnitOfMeasure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Unit { get; set; }

        public string? Description { get; set; }
    }
}
