using System.Diagnostics;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using WMS.DataAccess.Repository.IRepository;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WMSWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll();
            ViewBag.ProductCount = products != null ? products.Count() : 0;

            var customers = _unitOfWork.Customer.GetAll();
            ViewBag.CustomerCount = customers != null ? customers.Count() : 0;

            var pickNotes = _unitOfWork.PickNote.GetAll(x => x.Status == "Pending");
            ViewBag.PendingPickNoteCount = pickNotes != null ? pickNotes.Count() : 0;

            var putAways = _unitOfWork.GoodsReceivedNote.GetAll(x => x.Status == "Pending");
            ViewBag.PendingPutAwayCount = putAways != null ? putAways.Count() : 0;

            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var weekDates = Enumerable.Range(0, 7).Select(i => startOfWeek.AddDays(i)).ToList();
            var weeklyCounts = new List<int>();

            foreach (var date in weekDates)
            {
                int count = _unitOfWork.GoodsReceivedNote.GetAll()
                    .Count(grn => grn.GRNDate.Date == date.Date);
                weeklyCounts.Add(count);
            }

            var months = Enumerable.Range(1, 12).Select(m => new DateTime(DateTime.Today.Year, m, 1)).ToList();
            var monthlyCounts = new List<int>();

            foreach (var month in months)
            {
                int count = _unitOfWork.GoodsReceivedNote.GetAll()
                    .Count(grn => grn.GRNDate.Month == month.Month && grn.GRNDate.Year == month.Year);
                monthlyCounts.Add(count);
            }

            ViewBag.WeeklyGRNDays = weekDates.Select(d => d.ToString("ddd dd")).ToList();
            ViewBag.WeeklyGRNCounts = weeklyCounts;

            ViewBag.MonthlyGRNMonths = months.Select(m => m.ToString("MMM")).ToList();
            ViewBag.MonthlyGRNCounts = monthlyCounts;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult WeeklyGRNChart()
        {
            DateTime today = DateTime.Today;
            DateTime weekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime weekEnd = weekStart.AddDays(7);

            var grns = _unitOfWork.GoodsReceivedNote.GetAll(x => x.GRNDate >= weekStart && x.GRNDate < weekEnd);

            var dailyCounts = Enumerable.Range(0, 7).Select(i =>
            {
                var day = weekStart.AddDays(i).Date;
                return new
                {
                    Day = day.ToString("ddd"), // e.g. Mon
                    Count = grns.Count(g => g.GRNDate.Date == day)
                };
            }).ToList();

            // Prepare separate lists for days and counts
            ViewBag.GRNDays = dailyCounts.Select(x => x.Day).ToList();
            ViewBag.GRNCounts = dailyCounts.Select(x => x.Count).ToList();

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
