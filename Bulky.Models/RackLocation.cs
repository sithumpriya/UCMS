using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WMS.Models
{
    public class RackLocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [DisplayName("Rack")]
        public int RackId { get; set; }

        [ForeignKey("RackId")]
        [ValidateNever]
        public Rack Rack { get; set; }

        public int? GoodsReceivedNoteProductId { get; set; }

        public int? AvailableQuantity { get; set; }
    }
}
