using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMS.Models
{
    public class PickNoteVM
    {
        public PickNote PickNote { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }

        public List<PickNoteRackLocation> Products { get; set; } = new List<PickNoteRackLocation>();
    }
}
