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
    public class PolyclinicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPolyclinicService _polyclinicService;
        private readonly IExaminationService _examinationService;

        public PolyclinicsController(ApplicationDbContext context, IPolyclinicService polyclinicService, IExaminationService examinationService)
        {
            _context = context;
            _polyclinicService = polyclinicService;
            _examinationService = examinationService;
        }

        // GET: Polyclinics
        public async Task<IActionResult> Index()
        {
            return View(_polyclinicService.GetAllPolyclinics());
        }

        // GET: Polyclinics/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polyclinic = _polyclinicService.GetPolyclinic((Guid)id);
            ShowExaminationsForPolyclinicDTO showExaminationsForPolyclinicDTO = new ShowExaminationsForPolyclinicDTO()
            {
                Polyclinic = polyclinic,
                PolyclinicId = (Guid)id,
                Examinations = _examinationService.ShowExaminationsForPolyclinic((Guid)id)
            };
            if (polyclinic == null)
            {
                return NotFound();
            }

            return View(showExaminationsForPolyclinicDTO);
        }

        // GET: Polyclinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Polyclinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolyclinicName,PolyclinicAddress,Reputation,AvailableSlots,Id")] Polyclinic polyclinic)
        {
            if (ModelState.IsValid)
            {
                
                _polyclinicService.CreatePolyclinic(polyclinic);
                return RedirectToAction(nameof(Index));
            }
            return View(polyclinic);
        }

        // GET: Polyclinics/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polyclinic = _polyclinicService.GetPolyclinic((Guid)id);
            if (polyclinic == null)
            {
                return NotFound();
            }
            return View(polyclinic);
        }

        // POST: Polyclinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PolyclinicName,PolyclinicAddress,Reputation,AvailableSlots,Id")] Polyclinic polyclinic)
        {
            if (id != polyclinic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _polyclinicService.UpdatePolyclinic(polyclinic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolyclinicExists(polyclinic.Id))
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
            return View(polyclinic);
        }

        // GET: Polyclinics/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polyclinic = _polyclinicService.GetPolyclinic((Guid)id);
            if (polyclinic == null)
            {
                return NotFound();
            }

            return View(polyclinic);
        }

        // POST: Polyclinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var polyclinic = _polyclinicService.GetPolyclinic((Guid)id);
            if (polyclinic != null)
            {
                _polyclinicService.DeletePolyclinic(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolyclinicExists(Guid id)
        {
            return _polyclinicService.GetAllPolyclinics().Any(e => e.Id == id);
        }
    }
}
