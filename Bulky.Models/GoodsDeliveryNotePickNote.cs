using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    public class GoodsDeliveryNotePickNote
    {
        [Key]
        public int Id { get; set; }

        public int PickNoteId { get; set; }

        [DisplayName("Goods Delivery Note")]
        public int GoodsDeliveryNoteId { get; set; }

        [ForeignKey("GoodsDeliveryNoteId")]
        [ValidateNever]
        public GoodsDeliveryNote GoodsDeliveryNote { get; set; }
    }
}
