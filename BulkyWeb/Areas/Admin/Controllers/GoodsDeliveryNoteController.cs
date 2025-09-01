using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoodsDeliveryNoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GoodsDeliveryNoteController(IUnitOfWork unitOfWork)
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
            var vm = new GoodsDeliveryNoteVM
            {
                CustomerList = _unitOfWork.Customer.GetAll()
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                    };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(GoodsDeliveryNoteVM model)
        {
            var customer = _unitOfWork.Customer.Get(u => u.Id == model.GoodsDeliveryNote.CustomerId);
            if (customer != null)
            {
                model.GoodsDeliveryNote.CustomerName = customer.Name;
            }

            // Save the Goods Delivery Note
            _unitOfWork.GoodsDeliveryNote.Add(model.GoodsDeliveryNote);
            _unitOfWork.Save();

            foreach (var item in model.GDNPickNote)
            {
                // Get PickNote and mark as Completed
                var pickNote = _unitOfWork.PickNote.Get(u => u.Id == item.PickNoteId);
                if (pickNote != null)
                {
                    pickNote.Status = "Completed";
                    _unitOfWork.PickNote.Update(pickNote);
                }

                // Update stock in each rack location
                var pickNoteLocations = _unitOfWork.PickNoteRackLocation
                    .GetAll(u => u.PickNoteId == item.PickNoteId)
                    .ToList();

                foreach (var location in pickNoteLocations)
                {
                    var rackLocation = _unitOfWork.RackLocation.Get(u => u.Id == location.RackLocationId);
                    if (rackLocation != null)
                    {
                        rackLocation.AvailableQuantity -= location.RequestQuantity;
                        _unitOfWork.RackLocation.Update(rackLocation);
                    }
                }

                // Link pick note to GDN
                item.GoodsDeliveryNoteId = model.GoodsDeliveryNote.Id;
                item.PickNoteId = item.PickNoteId;
                _unitOfWork.GoodsDeliveryNotePickNote.Add(item);
            }

            _unitOfWork.Save();

            TempData["success"] = "GoodsDeliveryNote created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var gdn = _unitOfWork.GoodsDeliveryNote.Get(filter: g => g.Id == id);

            if (gdn == null)
            {
                return NotFound();
            }

            var gdnPickNotes = _unitOfWork.GoodsDeliveryNotePickNote.GetAll(
                filter: p => p.GoodsDeliveryNoteId == id
            ).ToList();

            // Get all PickNotes where Id matches PickNoteId from gdnPickNotes
            var pickNoteIds = gdnPickNotes.Select(p => p.PickNoteId).ToList();

            var pickNotes = _unitOfWork.PickNote.GetAll(p => pickNoteIds.Contains(p.Id));

            ViewBag.PickNotes = pickNotes;

            return View(gdn);
        }



        [HttpPost]
        public IActionResult Edit(GoodsDeliveryNote obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GoodsDeliveryNote.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "GoodsDeliveryNote updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<GoodsDeliveryNote> objGoodsDeliveryNoteList = _unitOfWork.GoodsDeliveryNote.GetAll().ToList();
            return Json(new { data = objGoodsDeliveryNoteList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var goodsDeliveryNoteToBeDeleted = _unitOfWork.GoodsDeliveryNote.Get(u => u.Id == id);
            if (goodsDeliveryNoteToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.GoodsDeliveryNote.Remove(goodsDeliveryNoteToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpGet]
        public IActionResult GetPickNotesByCustomer(int customerId)
        {
            var pickNotes = _unitOfWork.PickNote.GetAll(
                x => x.Status == "Pending" && x.CustomerId == customerId
            ).ToList();

            return Json(pickNotes);
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
