using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoodsReceivedNoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GoodsReceivedNoteController(IUnitOfWork unitOfWork)
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

                // 3. Only show RackLocations:
                // - NOT used in pending GRNs
                // - AND AvailableQuantity is null or zero
                RackLocationList = _unitOfWork.RackLocation.GetAll()
                    .Where(r =>
                        !usedRackLocationIdsInPending.Contains(r.Id) &&
                        (!r.AvailableQuantity.HasValue || r.AvailableQuantity == 0)
                    )
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
            List<GoodsReceivedNote> objGoodsReceivedNoteList = _unitOfWork.GoodsReceivedNote.GetAll(includeProperties: "Customer").ToList();
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

        #endregion
    }
}
