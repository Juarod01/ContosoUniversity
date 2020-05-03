using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.DTOs;
using AutoMapper;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentsController(IStudentService studentService,
            IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var data = await _studentService.GetCoursesByStudent(id.Value);
                ViewBag.Courses = data.Select(x => _mapper.Map<CourseDTO>(x)).ToList();
            }
            var dataList = await _studentService.GetAll();

            var listStudents = dataList.Select(x => _mapper.Map<StudentDTO>(x)).ToList();

            return View(listStudents);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                //var student = new Student
                //{
                //    FirstMidName = studentDTO.FirstMidName,
                //    LastName = studentDTO.LastName,
                //    EnrollmentDate = studentDTO.EnrollmentDate
                //};
                var student = _mapper.Map<Student>(studentDTO);
                student = await _studentService.Insert(student);
                var id = student.ID;
                return RedirectToAction("Index");
            }
            return View(studentDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentService.GetById(id.Value);

            if (student == null)
                return NotFound();

            var studentDTO = _mapper.Map<StudentDTO>(student);

            return View(studentDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(studentDTO);

                student = await _studentService.Update(student);
                return RedirectToAction("Index");
            }
            return View(studentDTO);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentService.GetById(id.Value);

            if (student == null)
                return NotFound();

            var studentDTO = _mapper.Map<StudentDTO>(student);

            return View(studentDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            await _studentService.Delete(id.Value);

            return RedirectToAction("Index");
        }
    }
}