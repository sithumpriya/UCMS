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
    public class LectureNoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LectureNoteController(IUnitOfWork unitOfWork)
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
        public IActionResult Create(LectureNote obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.LectureNote.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "LectureNote created successfully";
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

            IEnumerable<SelectListItem> CourseList = _unitOfWork.Course.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CourseList = CourseList;

            LectureNote? lectureNoteFromDb1 = _unitOfWork.LectureNote.Get(c => c.Id == id);

            if (lectureNoteFromDb1 == null)
            {
                return NotFound();
            }
            return View(lectureNoteFromDb1);
        }

        [HttpPost]
        public IActionResult Edit(LectureNote obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.LectureNote.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "LectureNote updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<LectureNote> objLectureNoteList = _unitOfWork.LectureNote.GetAll(includeProperties: "Course").ToList();
            return Json(new { data = objLectureNoteList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var lectureNoteToBeDeleted = _unitOfWork.LectureNote.Get(u => u.Id == id);
            if (lectureNoteToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.LectureNote.Remove(lectureNoteToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
