using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMS.Models
{
    public class InternalMoveVM
    {
        public InternalMove InternalMove { get; set; }
        public IEnumerable<SelectListItem> UsedRackLocationList { get; set; }
        public IEnumerable<SelectListItem> AvailableRackLocationList { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
