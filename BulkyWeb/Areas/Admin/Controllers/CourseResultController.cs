using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bulky.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Employee)]
    public class CourseResultController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseResultController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CourseList = _unitOfWork.Course.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CourseList = CourseList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseResult obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CourseResult.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Course Result created successfully";
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

            CourseResult? courseFromDb1 = _unitOfWork.CourseResult.Get(c => c.Id == id);

            if (courseFromDb1 == null)
            {
                return NotFound();
            }
            return View(courseFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(CourseResult obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CourseResult.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Course Result updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CourseResult> objCourseResultList = _unitOfWork.CourseResult.GetAll(includeProperties: "Course").ToList();
            return Json(new { data = objCourseResultList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var courseToBeDeleted = _unitOfWork.CourseResult.Get(u => u.Id == id);
            if (courseToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.CourseResult.Remove(courseToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
