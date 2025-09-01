using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Models;

public class RackLocationVM
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
    public string ProductName { get; set; }

    public int? AvailableQuantity { get; set; }

}
