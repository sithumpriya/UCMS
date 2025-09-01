using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitOfMeasureController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfMeasureController(IUnitOfWork unitOfWork)
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
        public IActionResult Create(UnitOfMeasure obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.UnitOfMeasure.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "UnitOfMeasure created successfully";
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

            UnitOfMeasure? unitOfMeasureFromDb1 = _unitOfWork.UnitOfMeasure.Get(c => c.Id == id);

            if (unitOfMeasureFromDb1 == null)
            {
                return NotFound();
            }
            return View(unitOfMeasureFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(UnitOfMeasure obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.UnitOfMeasure.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "UnitOfMeasure updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<UnitOfMeasure> objUnitOfMeasureList = _unitOfWork.UnitOfMeasure.GetAll().ToList();
            return Json(new { data = objUnitOfMeasureList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var unitOfMeasureToBeDeleted = _unitOfWork.UnitOfMeasure.Get(u => u.Id == id);
            if (unitOfMeasureToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.UnitOfMeasure.Remove(unitOfMeasureToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
