using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Course Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
