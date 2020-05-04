using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DTOs;
using AutoMapper;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private IEnrollmentService _enrollmentService;
        private IStudentService _studentService;
        private ICourseService _courseService;
        private readonly IMapper _mapper;
        public EnrollmentsController(IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService,
            IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dataList = await _enrollmentService.GetAll();
            var listEnrollments = dataList.Select(x => _mapper.Map<EnrollmentDTO>(x)).ToList();
            
            return View(listEnrollments);
        }

        public async Task<ActionResult> Create()
        {
            var dataCourses = await _courseService.GetAll();
            ViewBag.Courses = dataCourses.Select(x => _mapper.Map<CourseDTO>(x)).ToList();
            var dataStudents = await _studentService.GetAll();
            ViewBag.Students = dataStudents.Select(x => _mapper.Map<StudentDTO>(x)).ToList();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid)
                return View(enrollmentDTO);

            var enrollment = _mapper.Map<Enrollment>(enrollmentDTO);
            await _enrollmentService.Insert(enrollment);
            var id = enrollment.EnrollmentID;
            return RedirectToAction("Index");
        }


    }
}