using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RackLocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RackLocationController(IUnitOfWork unitOfWork)
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
        public IActionResult Create(RackLocation obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RackLocation.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Rack Location created successfully";
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

            RackLocation? rackLocationFromDb1 = _unitOfWork.RackLocation.Get(c => c.Id == id);

            if (rackLocationFromDb1 == null)
            {
                return NotFound();
            }
            return View(rackLocationFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(RackLocation obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RackLocation.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Rack Location updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<RackLocation> objRackLocationList = _unitOfWork.RackLocation.GetAll(includeProperties: "Rack").ToList();
            return Json(new { data = objRackLocationList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var rackLocationToBeDeleted = _unitOfWork.RackLocation.Get(u => u.Id == id);
            if (rackLocationToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.RackLocation.Remove(rackLocationToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
