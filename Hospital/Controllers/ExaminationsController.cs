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
    public class ExaminationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExaminationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Examinations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Examination.Include(e => e.Doctor).Include(e => e.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Examinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Examination == null)
            {
                return NotFound();
            }

            var examination = await _context.Examination
                .Include(e => e.Doctor)
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.ExaminationId == id);
            if (examination == null)
            {
                return NotFound();
            }

            return View(examination);
        }

        // GET: Examinations/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "LastName");
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Email");
            return View();
        }

        // POST: Examinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExaminationId,PatientId,DateOfExamination,DoctorId,Description")] Examination examination)
        {
            await Console.Out.WriteLineAsync("232332");
            if (ModelState.IsValid)
            {
                await Console.Out.WriteLineAsync("rereere");
                _context.Add(examination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "LastName", examination.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Email", examination.PatientId);
            return View(examination);
        }

        // GET: Examinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Examination == null)
            {
                return NotFound();
            }

            var examination = await _context.Examination.FindAsync(id);
            if (examination == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "LastName", examination.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Email", examination.PatientId);
            return View(examination);
        }

        // POST: Examinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExaminationId,PatientId,DateOfExamination,DoctorId,Description")] Examination examination)
        {
            if (id != examination.ExaminationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExaminationExists(examination.ExaminationId))
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
            ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "LastName", examination.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Set<Patient>(), "Id", "Email", examination.PatientId);
            return View(examination);
        }

        // GET: Examinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Examination == null)
            {
                return NotFound();
            }

            var examination = await _context.Examination
                .Include(e => e.Doctor)
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.ExaminationId == id);
            if (examination == null)
            {
                return NotFound();
            }

            return View(examination);
        }

        // POST: Examinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Examination == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Examination'  is null.");
            }
            var examination = await _context.Examination.FindAsync(id);
            if (examination != null)
            {
                _context.Examination.Remove(examination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExaminationExists(int id)
        {
          return (_context.Examination?.Any(e => e.ExaminationId == id)).GetValueOrDefault();
        }
    }
}
