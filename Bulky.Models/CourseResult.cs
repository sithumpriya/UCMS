using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WMS.Models
{
    public class CourseResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Student Code")]
        public string StudentCode { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Marks must be between 0-100")]
        public int Marks { get; set; }

        // Foreign key to Course
        [Required]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        [ValidateNever]
        public Course Course { get; set; }
    }
}
