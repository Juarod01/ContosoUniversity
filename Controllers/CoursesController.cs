using AutoMapper;
using ContosoUniversity.DTOs;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private ICourseService _courseService;
        private readonly IMapper _mapper;
        public CoursesController(ICourseService courseService,
            IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var data = await _courseService.GetStudentsByCourse(id.Value);
                ViewBag.Students = data.Select(x => _mapper.Map<StudentDTO>(x)).ToList();
            }
            var dataList = await _courseService.GetAll();

            var listCourses = dataList.Select(x => _mapper.Map<CourseDTO>(x)).ToList();

            return View(listCourses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _courseService.GetById(id.Value);

            if (course == null)
                return NotFound();

            var courseDTO = _mapper.Map<CourseDTO>(course);

            return View(courseDTO);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(courseDTO);
                course = await _courseService.Insert(course);
                var id = course.CourseID;
                return RedirectToAction("Index");
            }
            return View(courseDTO);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _courseService.GetById(id.Value);
            if (course == null)
                return NotFound();

            var courseDTO = _mapper.Map<CourseDTO>(course);

            return View(courseDTO);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(courseDTO);

                course = await _courseService.Update(course);
                return RedirectToAction("Index");
            }
            return View(courseDTO);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _courseService.GetById(id.Value);
            if (course == null)
                return NotFound();

            return RedirectToAction("Index");
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.Type = "danger";

                return View("Delete");
            }
        }
    }
}
