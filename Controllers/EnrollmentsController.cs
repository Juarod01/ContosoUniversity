using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private IEnrollmentService _enrollmentService;
        private IStudentService _studentService;
        private ICourseService _courseService;
        public EnrollmentsController(IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var listEnrollment = await _enrollmentService.GetAll();
            return View(listEnrollment);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Courses = await _courseService.GetAll();
            ViewBag.Students = await _studentService.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Enrollment model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _enrollmentService.Insert(model);

            return RedirectToAction("Index");
        }
    }
}