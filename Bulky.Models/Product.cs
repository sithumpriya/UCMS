using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WMS.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Out Bound Method")]
        public string OutBoundMethod { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Unit of Measure")]
        public string UnitOfMeasure { get; set; }

        [DisplayName("Category")]
        [ValidateNever]
        public int? CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
