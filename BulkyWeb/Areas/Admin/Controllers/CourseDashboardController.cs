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
    public class CourseDashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseDashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var courses = _unitOfWork.Course.GetAll();
            ViewBag.Courses = courses;

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var lectureNotes = _unitOfWork.LectureNote.GetAll(c => c.CourseId == id);
            ViewBag.LectureNotes = lectureNotes;

            ViewBag.Course = _unitOfWork.Course.Get(c => c.Id == id);

            return View();
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
