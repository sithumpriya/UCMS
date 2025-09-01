using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RackController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RackController(IUnitOfWork unitOfWork)
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
        public IActionResult Create(Rack obj)
        {
            if (ModelState.IsValid)
            {
                // Add the Rack first
                _unitOfWork.Rack.Add(obj);
                _unitOfWork.Save(); // Ensure obj.Id is generated

                // Dynamically generate RackLocations
                for (int col = 1; col <= obj.RackColumn; col++)
                {
                    for (int level = 1; level <= obj.RackLevel; level++)
                    {
                        var rackLocation = new RackLocation
                        {
                            Code = $"{obj.Code}-{col}-{level}",
                            RackId = obj.Id
                        };
                        _unitOfWork.RackLocation.Add(rackLocation);
                    }
                }

                _unitOfWork.Save();

                TempData["success"] = "Rack and associated locations created successfully";
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

            Rack? rackFromDb1 = _unitOfWork.Rack.Get(c => c.Id == id);

            if (rackFromDb1 == null)
            {
                return NotFound();
            }
            return View(rackFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(Rack obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Rack.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Rack updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Rack> objRackList = _unitOfWork.Rack.GetAll().ToList();
            return Json(new { data = objRackList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var rackToBeDeleted = _unitOfWork.Rack.Get(u => u.Id == id);
            if (rackToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Rack.Remove(rackToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
