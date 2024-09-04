using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Domain_Models;
using Repository;
using Service.Interface;
using Domain.DTO;

namespace Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDbContext _context;
        private readonly IExaminationService _examinationService;
        private readonly IPolyclinicService _polyclinicService;

        public EmployeesController(IEmployeeService employeeService, ApplicationDbContext context, IExaminationService examinationService, IPolyclinicService polyclinicService)
        {
            _employeeService = employeeService;
            _context = context;
            _examinationService = examinationService;
            _polyclinicService = polyclinicService;
        }

        public IActionResult ExamineEmployee(Guid id)
        {
            ExamineEmployeeDTO examineEmployeeDTO = new ExamineEmployeeDTO()
            {
                Polyclinics = _polyclinicService.GetAllPolyclinics(),
                Company = _employeeService.GetEmployee(id).Company,
                EmployeeId = id,
                Employee = _employeeService.GetEmployee(id)
            };
            return View(examineEmployeeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamineEmployee(ExamineEmployeeDTO examineEmployeeDTO)
        {
            var polyclinic = _polyclinicService.GetPolyclinic((Guid)examineEmployeeDTO.PolyclinicId);
            if (ModelState.IsValid)
            {
                if(polyclinic.AvailableSlots <= 0)
                {
                    return View("NoCapacity");
                }
                _polyclinicService.LowerCapacity(_polyclinicService.GetPolyclinic((Guid)examineEmployeeDTO.PolyclinicId));
                _examinationService.ExamineEmployee(examineEmployeeDTO);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            
            return View(_employeeService.GetAllEmployees());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployee((Guid)id);
            ShowExaminationsForEmployeeDTO showExaminationsForEmployeeDTO = new ShowExaminationsForEmployeeDTO()
            {
                Employee = employee,
                EmployeeId = (Guid)id,
                Examinations = _examinationService.ShowExaminationsForEmployee((Guid)id)
            };
            if (employee == null)
            {
                return NotFound();
            }

            return View(showExaminationsForEmployeeDTO);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,DateOfBirth,Title,CompanyId,Id")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployee((Guid)id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FullName,DateOfBirth,Title,CompanyId,Id")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.UpdateEmployee(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployee((Guid)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee = _employeeService.GetEmployee((Guid)id);
            if (employee != null)
            {
                _employeeService.DeleteEmployee(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _employeeService.GetAllEmployees().Any(e => e.Id == id);
        }
    }
}
