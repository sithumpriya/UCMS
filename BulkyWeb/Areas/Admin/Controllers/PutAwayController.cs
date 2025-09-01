using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PutAwayController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PutAwayController(IUnitOfWork unitOfWork)
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
            // Get all RackLocationIds used in pending GRNs
            var usedRackLocationIdsInPending = _unitOfWork.GoodsReceivedNoteProduct
                .GetAll(
                    filter: p => p.GoodsReceivedNote.Status == "Pending",
                    includeProperties: "GoodsReceivedNote"
                )
                .Select(p => p.RackLocationId)
                .Distinct()
                .ToList();

            // Build the ViewModel
            var vm = new GoodsReceivedNoteVM
            {
                CustomerList = _unitOfWork.Customer.GetAll()
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),

                ProductList = _unitOfWork.Product.GetAll()
                    .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }),

                RackLocationList = _unitOfWork.RackLocation.GetAll()
                    .Where(r => !usedRackLocationIdsInPending.Contains(r.Id))
                    .Select(r => new SelectListItem { Text = r.Code, Value = r.Id.ToString() })
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(GoodsReceivedNoteVM model)
        {
            model.GoodsReceivedNote.Status = "Pending";

            _unitOfWork.GoodsReceivedNote.Add(model.GoodsReceivedNote);
            _unitOfWork.Save();

            foreach (var item in model.Products)
            {
                item.GoodsReceivedNoteId = model.GoodsReceivedNote.Id;
                _unitOfWork.GoodsReceivedNoteProduct.Add(item);
            }
            _unitOfWork.Save();

            TempData["success"] = "GoodsReceivedNote created successfully";
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var grn = _unitOfWork.GoodsReceivedNote.Get(
                filter: g => g.Id == id,
                includeProperties: "Customer"
            );

            if (grn == null)
            {
                return NotFound();
            }

            var grnProducts = _unitOfWork.GoodsReceivedNoteProduct.GetAll(
                filter: p => p.GoodsReceivedNoteId == id,
                includeProperties: "Product,RackLocation"
            );

            ViewBag.GRNProducts = grnProducts;

            return View(grn);
        }



        [HttpPost]
        public IActionResult Edit(GoodsReceivedNote obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GoodsReceivedNote.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "GoodsReceivedNote updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<GoodsReceivedNote> objGoodsReceivedNoteList = _unitOfWork.GoodsReceivedNote.GetAll(g => g.Status == "Pending", includeProperties: "Customer").ToList();
            return Json(new { data = objGoodsReceivedNoteList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var goodsReceivedNoteToBeDeleted = _unitOfWork.GoodsReceivedNote.Get(u => u.Id == id);
            if (goodsReceivedNoteToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.GoodsReceivedNote.Remove(goodsReceivedNoteToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpPost]
        public IActionResult Reject(int? id)
        {
            var goodsReceivedNoteToBeRejected = _unitOfWork.GoodsReceivedNote.Get(u => u.Id == id);
            if (goodsReceivedNoteToBeRejected == null)
            {
                return Json(new { success = false, message = "Error while rejecting" });
            }

            goodsReceivedNoteToBeRejected.Status = "Rejected";
            _unitOfWork.GoodsReceivedNote.Update(goodsReceivedNoteToBeRejected);
            _unitOfWork.Save();

            return Json(new { success = true, message = "GRN Rejected Successfully" });
        }

        [HttpPost]
        public IActionResult Approve(int? id)
        {
            var goodsReceivedNote = _unitOfWork.GoodsReceivedNote.Get(u => u.Id == id);
            if (goodsReceivedNote == null)
            {
                return Json(new { success = false, message = "Error while approving" });
            }

            // 1. Set GRN status to Approved
            goodsReceivedNote.Status = "Approved";
            _unitOfWork.GoodsReceivedNote.Update(goodsReceivedNote);

            // 2. Get all related GRN products
            var grnProducts = _unitOfWork.GoodsReceivedNoteProduct
                .GetAll(p => p.GoodsReceivedNoteId == id)
                .ToList();

            if (!grnProducts.Any())
            {
                return Json(new { success = false, message = "No products found for this GRN." });
            }

            // 3. Update related RackLocations
            foreach (var product in grnProducts)
            {
                var rackLocation = _unitOfWork.RackLocation
                    .Get(r => r.Id == product.RackLocationId);

                if (rackLocation != null)
                {
                    rackLocation.GoodsReceivedNoteProductId = product.Id;
                    rackLocation.AvailableQuantity = product.Quantity;

                    _unitOfWork.RackLocation.Update(rackLocation);
                }
                else
                {
                    return Json(new { success = false, message = $"Rack location not found for product ID {product.Id}" });
                }
            }

            // 4. Save all updates
            _unitOfWork.Save();

            return Json(new { success = true, message = "GRN Approved Successfully" });
        }

        #endregion
    }
}
