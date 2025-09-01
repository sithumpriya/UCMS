using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    public class InternalMove
    {
        [Key]
        public int Id { get; set; }

        public string? Remark { get; set; }

        public int PreviousLocationId { get; set; }

        public string PreviousLocationCode { get; set; }

        public int NewLocationId { get; set; }

        public string NewLocationCode { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}
