using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMS.Models
{
    public class GoodsReceivedNoteVM
    {
        public GoodsReceivedNote GoodsReceivedNote { get; set; } = new();

        public List<GoodsReceivedNoteProduct> Products { get; set; } = new();

        public IEnumerable<SelectListItem> CustomerList { get; set; }

        public IEnumerable<SelectListItem> ProductList { get; set; }

        public IEnumerable<SelectListItem> RackLocationList { get; set; }
    }
}
