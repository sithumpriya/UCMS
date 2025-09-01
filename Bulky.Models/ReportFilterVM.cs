using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class ReportFilterVM
{
    public string SelectedReport { get; set; }
    public string CustomerName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FromDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ToDate { get; set; }

    public IEnumerable<SelectListItem> AvailableReports { get; set; }
    public IEnumerable<SelectListItem> CustomerList { get; set; }
}
