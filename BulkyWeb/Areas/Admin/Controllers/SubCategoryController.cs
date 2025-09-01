using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(SubCategory obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SubCategory.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Sub Category created successfully";
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

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            SubCategory? subCategoryFromDb1 = _unitOfWork.SubCategory.Get(c => c.Id == id);

            if (subCategoryFromDb1 == null)
            {
                return NotFound();
            }
            return View(subCategoryFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(SubCategory obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SubCategory.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Sub Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<SubCategory> objSubCategoryList = _unitOfWork.SubCategory.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objSubCategoryList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var subCategoryToBeDeleted = _unitOfWork.SubCategory.Get(u => u.Id == id);
            if (subCategoryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.SubCategory.Remove(subCategoryToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
