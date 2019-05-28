using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SetShapeDatabase;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase.Controllers
{
    public class TrainingDaysViewController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SetShapeContext _context;

        public TrainingDaysViewController(SetShapeContext context)
        {
            _context = context;
        }

        // GET: TrainingDaysView
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingDays.ToListAsync());
        }

        // GET: TrainingDaysView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingDay = await _context.TrainingDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingDay == null)
            {
                return NotFound();
            }

            return View(trainingDay);
        }

        // GET: TrainingDaysView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingDaysView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TrainingDay trainingDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingDay);
        }

        // GET: TrainingDaysView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingDay = await _context.TrainingDays.FindAsync(id);
            if (trainingDay == null)
            {
                return NotFound();
            }
            return View(trainingDay);
        }

        // POST: TrainingDaysView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TrainingDay trainingDay)
        {
            if (id != trainingDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingDayExists(trainingDay.Id))
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
            return View(trainingDay);
        }

        // GET: TrainingDaysView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingDay = await _context.TrainingDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingDay == null)
            {
                return NotFound();
            }

            return View(trainingDay);
        }

        // POST: TrainingDaysView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingDay = await _context.TrainingDays.FindAsync(id);
            _context.TrainingDays.Remove(trainingDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingDayExists(int id)
        {
            return _context.TrainingDays.Any(e => e.Id == id);
        }
    }
}
