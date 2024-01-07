using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;

namespace Hospital.Controllers
{
    public class ReserveExaminationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReserveExaminationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReserveExaminations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReserveExamination.Include(r => r.Doctor).Include(r => r.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReserveExaminations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReserveExamination == null)
            {
                return NotFound();
            }

            var reserveExamination = await _context.ReserveExamination
                .Include(r => r.Doctor)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserveExamination == null)
            {
                return NotFound();
            }

            return View(reserveExamination);
        }

        // GET: ReserveExaminations/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Id");
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id");
            return View();
        }

        // POST: ReserveExaminations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,ExaminationDate,ExaminationTime,DoctorId")] ReserveExamination reserveExamination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserveExamination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Id", reserveExamination.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id", reserveExamination.PatientId);
            return View(reserveExamination);
        }

        // GET: ReserveExaminations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReserveExamination == null)
            {
                return NotFound();
            }

            var reserveExamination = await _context.ReserveExamination.FindAsync(id);
            if (reserveExamination == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Id", reserveExamination.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id", reserveExamination.PatientId);
            return View(reserveExamination);
        }

        // POST: ReserveExaminations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,ExaminationDate,ExaminationTime,DoctorId")] ReserveExamination reserveExamination)
        {
            if (id != reserveExamination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserveExamination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveExaminationExists(reserveExamination.Id))
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
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Id", reserveExamination.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Id", reserveExamination.PatientId);
            return View(reserveExamination);
        }

        // GET: ReserveExaminations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReserveExamination == null)
            {
                return NotFound();
            }

            var reserveExamination = await _context.ReserveExamination
                .Include(r => r.Doctor)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserveExamination == null)
            {
                return NotFound();
            }

            return View(reserveExamination);
        }

        // POST: ReserveExaminations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReserveExamination == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReserveExamination'  is null.");
            }
            var reserveExamination = await _context.ReserveExamination.FindAsync(id);
            if (reserveExamination != null)
            {
                _context.ReserveExamination.Remove(reserveExamination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveExaminationExists(int id)
        {
          return (_context.ReserveExamination?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
