using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InternalMoveController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InternalMoveController(IUnitOfWork unitOfWork)
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
            var pendingGRNRackLocationIds = _unitOfWork.GoodsReceivedNoteProduct
                .GetAll(includeProperties: "GoodsReceivedNote")
                .Where(p => p.GoodsReceivedNote.Status == "Pending" && p.RackLocationId != null)
                .Select(p => p.RackLocationId) // Assuming 0 is not a valid ID
                .Distinct()
                .ToList();

            var pendingPNRackLocationIds = _unitOfWork.PickNoteRackLocation
                .GetAll(includeProperties: "PickNote")
                .Where(p => p.PickNote.Status == "Pending" && p.RackLocationId != null)
                .Select(p => p.RackLocationId) // Assuming 0 is not a valid ID
                .Distinct()
                .ToList();

            // Build the ViewModel
            var vm = new InternalMoveVM
            {
                UsedRackLocationList = _unitOfWork.RackLocation
                    .GetAll(u => u.AvailableQuantity > 0 && !pendingPNRackLocationIds.Contains(u.Id))
                    .Select(c => new SelectListItem
                    {
                        Text = c.Code,
                        Value = c.Id.ToString()
                    }),

                AvailableRackLocationList = _unitOfWork.RackLocation.GetAll(u => (u.AvailableQuantity == 0 || u.AvailableQuantity == null) && !pendingGRNRackLocationIds.Contains(u.Id))
                    .Select(c => new SelectListItem
                    {
                        Text = c.Code,
                        Value = c.Id.ToString()
                    }),
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(InternalMoveVM model)
        {
            var previousRackLocation = _unitOfWork.RackLocation.Get(u => u.Id == model.InternalMove.PreviousLocationId);
            var newRackLocation = _unitOfWork.RackLocation.Get(u => u.Id == model.InternalMove.NewLocationId);

            if (previousRackLocation != null)
            {               
                if (newRackLocation != null)
                {
                    // Transfer available quantity and GRN product reference
                    newRackLocation.AvailableQuantity = previousRackLocation.AvailableQuantity;
                    newRackLocation.GoodsReceivedNoteProductId = previousRackLocation.GoodsReceivedNoteProductId;

                    //// Update GRN Product references to the new location
                    //var grnsRackLocations = _unitOfWork.GoodsReceivedNoteProduct
                    //    .GetAll(u => u.RackLocationId == model.InternalMove.PreviousLocationId);

                    //foreach (var grnLocation in grnsRackLocations)
                    //{
                    //    grnLocation.RackLocationId = model.InternalMove.NewLocationId;
                    //    _unitOfWork.GoodsReceivedNoteProduct.Update(grnLocation);
                    //}

                    //// Update PickNoteRackLocation references to the new location
                    //var pnsRackLocations = _unitOfWork.PickNoteRackLocation
                    //    .GetAll(u => u.RackLocationId == model.InternalMove.PreviousLocationId);

                    //foreach (var pnLocation in pnsRackLocations)
                    //{
                    //    pnLocation.RackLocationId = model.InternalMove.NewLocationId;
                    //    pnLocation.RackLocationCode = newRackLocation.Code;
                    //    _unitOfWork.PickNoteRackLocation.Update(pnLocation);
                    //}

                    model.InternalMove.PreviousLocationCode = previousRackLocation.Code;
                    model.InternalMove.NewLocationCode = newRackLocation.Code;
                    model.InternalMove.ProductId = previousRackLocation.GoodsReceivedNoteProductId;
                    model.InternalMove.Quantity = previousRackLocation.AvailableQuantity;

                    // Clear previous rack's available quantity
                    previousRackLocation.AvailableQuantity = 0;

                    _unitOfWork.RackLocation.Update(newRackLocation);
                    _unitOfWork.RackLocation.Update(previousRackLocation);
                }
            }

            _unitOfWork.InternalMove.Add(model.InternalMove);
            _unitOfWork.Save();

            TempData["success"] = "Internal Move created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var pn = _unitOfWork.InternalMove.Get(
                filter: g => g.Id == id
            );

            if (pn == null)
            {
                return NotFound();
            }

            return View(pn);
        }

        [HttpPost]
        public IActionResult Edit(InternalMove obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.InternalMove.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "InternalMove updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<InternalMove> objInternalMoveList = _unitOfWork.InternalMove.GetAll().ToList();
            return Json(new { data = objInternalMoveList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var internalMoveToBeDeleted = _unitOfWork.InternalMove.Get(u => u.Id == id);
            if (internalMoveToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.InternalMove.Remove(internalMoveToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
