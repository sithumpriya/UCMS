using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var model = new ReportFilterVM
            {
                AvailableReports = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Customer Report", Value = "Customer" },
                    new SelectListItem { Text = "GRN Report", Value = "GRN" },
                    new SelectListItem { Text = "GDN Report", Value = "GDN" },
                    new SelectListItem { Text = "Pick Note Report", Value = "PickNote" },
                    new SelectListItem { Text = "Put Away Report", Value = "PutAway" },
                    new SelectListItem { Text = "Stock Statement Report", Value = "StockStatement" },
                    new SelectListItem { Text = "Product Report", Value = "Product" }
                },
                CustomerList = _unitOfWork.Customer.GetAll()
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Name })
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ViewReport(ReportFilterVM filter)
        {
            switch (filter.SelectedReport)
            {
                case "GRN":
                    return RedirectToAction("GRNReport", filter);
                case "GDN":
                    return RedirectToAction("GDNReport", filter);
                case "Customer":
                    return RedirectToAction("CustomerReport", filter);
                case "PickNote":
                    return RedirectToAction("PickNoteReport", filter);
                case "PutAway":
                    return RedirectToAction("PutAwayReport", filter);
                case "Product":
                    return RedirectToAction("ProductReport", filter);
                case "StockStatement":
                    return RedirectToAction("StockStatementReport", filter);
                default:
                    return RedirectToAction("Index");
            }
        }

        public IActionResult GRNReport(ReportFilterVM filter)
        {
            var data = _unitOfWork.GoodsReceivedNote
                .GetAll(includeProperties: "Customer")
                .Where(x =>
                    (string.IsNullOrEmpty(filter.CustomerName) || x.Customer.Name.Contains(filter.CustomerName)) &&
                    (!filter.FromDate.HasValue || x.GRNDate >= filter.FromDate.Value) &&
                    (!filter.ToDate.HasValue || x.GRNDate <= filter.ToDate.Value)
                )
                .ToList();

            ViewBag.Filter = filter;
            return View("GRNReportResult", data);
        }

        public IActionResult GDNReport(ReportFilterVM filter)
        {
            var data = _unitOfWork.GoodsDeliveryNote
                .GetAll()
                .Where(x =>
                    (string.IsNullOrEmpty(filter.CustomerName) || x.CustomerName.Contains(filter.CustomerName)) &&
                    (!filter.FromDate.HasValue || x.GDNDate >= filter.FromDate.Value) &&
                    (!filter.ToDate.HasValue || x.GDNDate <= filter.ToDate.Value)
                )
                .ToList();

            ViewBag.Filter = filter;
            return View("GDNReportResult", data);
        }

        public IActionResult CustomerReport(ReportFilterVM filter)
        {
            var data = _unitOfWork.Customer.GetAll()
                .Where(c => string.IsNullOrEmpty(filter.CustomerName) || c.Name.Contains(filter.CustomerName))
                .ToList();

            ViewBag.Filter = filter;
            return View("CustomerReportResult", data);
        }

        public IActionResult PickNoteReport(ReportFilterVM filter)
        {
            var data = _unitOfWork.PickNote.GetAll()
                .Where(x =>
                    (string.IsNullOrEmpty(filter.CustomerName) || x.CustomerName.Contains(filter.CustomerName)) &&
                    (!filter.FromDate.HasValue || x.PNDate >= filter.FromDate.Value) &&
                    (!filter.ToDate.HasValue || x.PNDate <= filter.ToDate.Value)
                )
                .ToList();

            ViewBag.Filter = filter;
            return View("PickNoteReportResult", data);
        }

        public IActionResult PutAwayReport(ReportFilterVM filter)
        {
            var data = _unitOfWork.GoodsReceivedNote.GetAll(includeProperties: "Customer")
                .Where(x =>
                    (string.IsNullOrEmpty(filter.CustomerName) || x.Customer.Name.Contains(filter.CustomerName)) &&
                    (!filter.FromDate.HasValue || x.GRNDate >= filter.FromDate.Value) &&
                    (!filter.ToDate.HasValue || x.GRNDate <= filter.ToDate.Value) &&
                    (x.Status == "Pending")
                )
                .ToList();

            ViewBag.Filter = filter;
            return View("PutAwayReportResult", data);
        }

        public IActionResult ProductReport(ReportFilterVM filter)
        {
            var data = _unitOfWork.Product.GetAll(includeProperties: "Category")
                .ToList();

            ViewBag.Filter = filter;
            return View("ProductReportResult", data);
        }

        public IActionResult StockStatementReport(ReportFilterVM filter)
        {
            List<RackLocationVM> data = new List<RackLocationVM>();
            var rackLocations = _unitOfWork.RackLocation.GetAll(includeProperties: "Rack").Where(x => x.AvailableQuantity > 0).ToList();

            foreach (var rackLocation in rackLocations)
            {
                var product = _unitOfWork.Product.Get(x => x.Id == rackLocation.GoodsReceivedNoteProductId);
                var vm = new RackLocationVM
                {
                    Id = rackLocation.Id,
                    Code = rackLocation.Code,
                    RackId = rackLocation.RackId,
                    Rack = rackLocation.Rack,
                    GoodsReceivedNoteProductId = rackLocation.GoodsReceivedNoteProductId,
                    AvailableQuantity = rackLocation.AvailableQuantity,
                    ProductName = product?.Name
                };

                data.Add(vm);
            }

            ViewBag.Filter = filter;
            return View("StockStatementReportResult", data);
        }

    }
}
