using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    public class PickNoteRackLocation
    {
        [Key]
        public int Id { get; set; }
     
        public int RackLocationId { get; set; }

        public int RequestQuantity { get; set; }

        [DisplayName("Pick Note")]
        public int PickNoteId { get; set; }

        [ForeignKey("PickNoteId")]
        [ValidateNever]
        public PickNote PickNote { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string RackLocationCode { get; set; }
    }
}
