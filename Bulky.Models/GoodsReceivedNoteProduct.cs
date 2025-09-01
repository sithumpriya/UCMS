using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WMS.Models
{
    public class GoodsReceivedNoteProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [DisplayName("Goods Received Note")]
        public int GoodsReceivedNoteId { get; set; }

        [ForeignKey("GoodsReceivedNoteId")]
        [ValidateNever]
        public GoodsReceivedNote GoodsReceivedNote { get; set; }

        [DisplayName("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        [DisplayName("Rack Location")]
        public int RackLocationId { get; set; }

        [ForeignKey("RackLocationId")]
        [ValidateNever]
        public RackLocation RackLocation { get; set; }
    }
}
