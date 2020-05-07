using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.DTOs;
using AutoMapper;
using ContosoUniversity.Services;

namespace ContosoUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        //private readonly SchoolContext _context;
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public InstructorsController(IInstructorService instructorService,
            IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            if (id != null)
            {
                var data = await _instructorService.GetCoursesByInstructor(id.Value);
                ViewBag.Courses = data.Select(x => _mapper.Map<CourseDTO>(x)).ToList();
            }
            if (courseID != null)
            {
                var dataCourse = await _instructorService.GetStudentsByCourse(courseID.Value);
                ViewBag.Students = dataCourse.Select(x => _mapper.Map<EnrollmentDTO>(x)).ToList();
            }
            
            var dataList = await _instructorService.GetAll();
            var listInstructors = dataList.Select(x => _mapper.Map<InstructorDTO>(x)).ToList();

            return View(listInstructors);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var instructor = await _instructorService.GetById(id.Value);

            if (instructor == null)
                return NotFound();

            var instructorDTO = _mapper.Map<InstructorDTO>(instructor);

            return View(instructorDTO);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorDTO instructorDTO)
        {
            if (ModelState.IsValid)
            {
                var instructor = _mapper.Map<Instructor>(instructorDTO);
                instructor = await _instructorService.Insert(instructor);
                var id = instructor.ID;
                return RedirectToAction("Index");
            }
            return View(instructorDTO);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var instructor = await _instructorService.GetById(id.Value);

            if (instructor == null)
                return NotFound();

            var instructorDTO = _mapper.Map<InstructorDTO>(instructor);

            return View(instructorDTO);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InstructorDTO instructorDTO)
        {
            if (ModelState.IsValid)
            {
                var instructor = _mapper.Map<Instructor>(instructorDTO);

                instructor = await _instructorService.Update(instructor);

                return RedirectToAction("Index");
            }
            return View(instructorDTO);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            await _instructorService.Delete(id.Value);

            return RedirectToAction("Index");
        }
    }
}
