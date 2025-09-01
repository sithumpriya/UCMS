using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PickNoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PickNoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Build the ViewModel
            var vm = new PickNoteVM
            {
                CustomerList = _unitOfWork.Customer.GetAll()
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                    };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PickNoteVM model)
        {
            model.PickNote.Status = "Pending";

            var customer = _unitOfWork.Customer.Get(u => u.Id == model.PickNote.CustomerId);
            if (customer != null)
            {
                model.PickNote.CustomerName = customer.Name;
            }

            _unitOfWork.PickNote.Add(model.PickNote);
            _unitOfWork.Save();

            foreach (var item in model.Products)
            {
                item.PickNoteId = model.PickNote.Id;
                _unitOfWork.PickNoteRackLocation.Add(item);
            }
            _unitOfWork.Save();

            TempData["success"] = "PickNote created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var pn = _unitOfWork.PickNote.Get(
                filter: g => g.Id == id
            );

            if (pn == null)
            {
                return NotFound();
            }

            var pnProducts = _unitOfWork.PickNoteRackLocation.GetAll(
                filter: p => p.PickNoteId == id
            );

            ViewBag.PNProducts = pnProducts;

            return View(pn);
        }

        [HttpPost]
        public IActionResult Edit(PickNote obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PickNote.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "PickNote updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<PickNote> objPickNoteList = _unitOfWork.PickNote.GetAll().ToList();
            return Json(new { data = objPickNoteList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var pickNoteToBeDeleted = _unitOfWork.PickNote.Get(u => u.Id == id);
            if (pickNoteToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.PickNote.Remove(pickNoteToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpGet]
        public IActionResult GetRackLocationsByCustomer(int customerId)
        {
            // Step 1: Get all GRN products with includes
            var allGRNProducts = _unitOfWork.GoodsReceivedNoteProduct.GetWithIncludes(
                p => p.GoodsReceivedNote.CustomerId == customerId,
                p => p.Product,
                p => p.RackLocation,
                p => p.GoodsReceivedNote
            ).ToList();

            // Step 2: Get all pending pick note rack location records (with PickNote included)
            var allPendingPickNotes = _unitOfWork.PickNoteRackLocation.GetWithIncludes(
                x => x.PickNote.Status == "Pending",
                x => x.PickNote
            ).ToList();

            // Step 3: Build the result
            var rackLocations = allGRNProducts
                .Select(p =>
                {
                    var reservedQty = allPendingPickNotes
                        .Where(x => x.RackLocationId == p.RackLocationId && x.ProductId == p.Product.Id)
                        .Sum(x => (decimal?)x.RequestQuantity) ?? 0;

                    var remainingQty = p.RackLocation.AvailableQuantity - reservedQty;

                    return new
                    {
                        ProductId = p.Product.Id,
                        ProductName = p.Product.Name,
                        RackLocationId = p.RackLocation.Id,
                        RackLocationCode = p.RackLocation.Code,
                        AvailableQty = remainingQty
                    };
                })
                .Where(x => x.AvailableQty > 0)
                .Distinct()
                .ToList();

            return Json(rackLocations);
        }

        //public IActionResult GetRackLocationsByCustomer(int customerId)
        //{
        //    var rackLocations = _unitOfWork.GoodsReceivedNoteProduct.GetWithIncludes(
        //        p => p.GoodsReceivedNote.CustomerId == customerId && p.RackLocation.AvailableQuantity > 0,
        //        p => p.Product,
        //        p => p.RackLocation,
        //        p => p.GoodsReceivedNote
        //    ).Select(p => new
        //    {
        //        ProductId = p.Product.Id,
        //        ProductName = p.Product.Name,
        //        RackLocationId = p.RackLocation.Id,
        //        RackLocationCode = p.RackLocation.Code,
        //        AvailableQty = p.RackLocation.AvailableQuantity
        //    }).ToList();

        //    return Json(rackLocations);
        //}

        #endregion
    }
}
