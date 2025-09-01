using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WMS.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class GoodsReceivedNote
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [DisplayName("GRN Date")]
        public DateTime GRNDate { get; set; }

        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [DisplayName("Finish Time")]
        public DateTime FinishTime { get; set; }

        [MaxLength(20)]
        [DisplayName("Seal No")]
        public string? SealNo { get; set; }

        [MaxLength(15)]
        [DisplayName("Vehicle No")]
        public string? VehicleNo { get; set; }

        [MaxLength(15)]
        [DisplayName("Container No")]
        public string? ContainerNo { get; set; }

        [MaxLength(15)]
        [DisplayName("Trailor No")]
        public string? TrailorNo { get; set; }

        public string? Remark { get; set; }

        [DisplayName("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [ValidateNever]
        public Customer Customer { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }
    }
}
