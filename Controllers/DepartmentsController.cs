using AutoMapper;
using ContosoUniversity.DTOs;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDepartmentService _departmentService;
        private IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService, 
            IInstructorService instructorService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _instructorService = instructorService;
            _mapper = mapper;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var dataList = await _departmentService.GetAll();
            var listDespartments = dataList.Select(x => _mapper.Map<DepartmentDTO>(x)).ToList();

            return View(listDespartments);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentService.GetById(id.Value);
                
            if (department == null)
            {
                return NotFound();
            }
            var departmentDTO = _mapper.Map<DepartmentDTO>(department);

            return View(departmentDTO);
        }

        // GET: Departments/Create
        public async Task<ActionResult> Create()
        {
            var dataInstructor = await _instructorService.GetAll();
            ViewBag.Instructors = dataInstructor.Select(x => _mapper.Map<InstructorDTO>(x)).ToList();

            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentDTO);
                department = await _departmentService.Insert(department);
                var id = department.DepartmentID;
                return RedirectToAction("Index");
            }
            
            return View(departmentDTO);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _departmentService.GetById(id.Value);

            if (department == null)
                return NotFound();

            var departmentDTO = _mapper.Map<DepartmentDTO>(department);
            return View(departmentDTO);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentDTO);

                department = await _departmentService.Update(department);
                return RedirectToAction("Index");
            }
            return View(departmentDTO);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            await _departmentService.Delete(id.Value);

            return RedirectToAction("Index");
        }
    }
}
