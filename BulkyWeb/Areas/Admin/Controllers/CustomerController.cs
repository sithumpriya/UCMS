using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
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
        public IActionResult Create(WMS.Models.Customer obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customer.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Customer created successfully";
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

            WMS.Models.Customer? customerFromDb1 = _unitOfWork.Customer.Get(c => c.Id == id);

            if (customerFromDb1 == null)
            {
                return NotFound();
            }
            return View(customerFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(WMS.Models.Customer obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customer.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Customer updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<WMS.Models.Customer> objCustomerList = _unitOfWork.Customer.GetAll().ToList();
            return Json(new { data = objCustomerList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var customerToBeDeleted = _unitOfWork.Customer.Get(u => u.Id == id);
            if (customerToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Customer.Remove(customerToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
