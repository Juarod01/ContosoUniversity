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
    public class OfficeAssignmentsController : Controller
    {
        private IOfficeAssignmentService _officeAssignmentService;
        private IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public OfficeAssignmentsController(IOfficeAssignmentService officeAssignmentService,
            IInstructorService instructorService,
            IMapper mapper)
        {
            _officeAssignmentService = officeAssignmentService;
            _instructorService = instructorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dataList = await _officeAssignmentService.GetAll();
            var listOffice = dataList.Select(x => _mapper.Map<OfficeAssignmentDTO>(x)).ToList();

            return View(listOffice);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _officeAssignmentService.GetById(id.Value);

            if (office == null)
            {
                return NotFound();
            }
            var officeDTO = _mapper.Map<OfficeAssignmentDTO>(office);

            return View(officeDTO);
        }

        public async Task<ActionResult> Create()
        {
            var dataInstructor = await _instructorService.GetAll();
            ViewBag.Instructors = dataInstructor.Select(x => _mapper.Map<InstructorDTO>(x)).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficeAssignmentDTO officeAssignmentDTO)
        {
            if (ModelState.IsValid)
            {
                var office = _mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                office = await _officeAssignmentService.Insert(office);
                var id = office.InstructorID;
                return RedirectToAction("Index");
            }

            return View(officeAssignmentDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var office = await _officeAssignmentService.GetById(id.Value);

            var dataInstructor = await _instructorService.GetAll();
            ViewBag.Instructors = dataInstructor.Select(x => _mapper.Map<InstructorDTO>(x)).ToList();

            if (office == null)
                return NotFound();

            var officeAssignmentDTO = _mapper.Map<OfficeAssignmentDTO>(office);
            return View(officeAssignmentDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OfficeAssignmentDTO officeAssignmentDTO)
        {
            if (ModelState.IsValid)
            {
                var office = _mapper.Map<OfficeAssignment>(officeAssignmentDTO);

                office = await _officeAssignmentService.Update(office);
                return RedirectToAction("Index");
            }
            return View(officeAssignmentDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            await _officeAssignmentService.Delete(id.Value);

            return RedirectToAction("Index");
        }
    }
}