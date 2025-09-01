using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bulky.Utility;

namespace WMSWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Employee)]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
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
        public IActionResult Create(Course obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Course created successfully";
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

            Course? courseFromDb1 = _unitOfWork.Course.Get(c => c.Id == id);

            if (courseFromDb1 == null)
            {
                return NotFound();
            }
            return View(courseFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(Course obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Course updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Course> objCourseList = _unitOfWork.Course.GetAll().ToList();
            return Json(new { data = objCourseList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var courseToBeDeleted = _unitOfWork.Course.Get(u => u.Id == id);
            if (courseToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Course.Remove(courseToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
