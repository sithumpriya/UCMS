using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMS.Models
{
    public class GoodsDeliveryNoteVM
    {
        public GoodsDeliveryNote GoodsDeliveryNote { get; set; }

        public IEnumerable<SelectListItem> CustomerList { get; set; }

        public List<GoodsDeliveryNotePickNote> GDNPickNote { get; set; } = new List<GoodsDeliveryNotePickNote>();
    }
}
