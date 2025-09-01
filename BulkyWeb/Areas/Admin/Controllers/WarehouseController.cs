using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WarehouseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public WarehouseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {          
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Warehouse obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Warehouse.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Warehouse created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Warehouse? warehouseFromDb1 = _unitOfWork.Warehouse.Get(c => c.Id == id);

            if (warehouseFromDb1 == null)
            {
                return NotFound();
            }
            return View(warehouseFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(Warehouse obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Warehouse.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Warehouse updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Warehouse> objWarehouseList = _unitOfWork.Warehouse.GetAll().ToList();
            return Json(new { data = objWarehouseList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var warehouseToBeDeleted = _unitOfWork.Warehouse.Get(u => u.Id == id);
            if (warehouseToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Warehouse.Remove(warehouseToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
