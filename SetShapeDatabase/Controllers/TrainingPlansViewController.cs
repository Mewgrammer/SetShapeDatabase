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
    public class TrainingPlansViewController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SetShapeContext _context;

        public TrainingPlansViewController(SetShapeContext context)
        {
            _context = context;
        }

        // GET: TrainingPlansView
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingPlans.ToListAsync());
        }

        // GET: TrainingPlansView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingPlan == null)
            {
                return NotFound();
            }

            return View(trainingPlan);
        }

        // GET: TrainingPlansView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingPlansView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TrainingPlan trainingPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingPlan);
        }

        // GET: TrainingPlansView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans.FindAsync(id);
            if (trainingPlan == null)
            {
                return NotFound();
            }
            return View(trainingPlan);
        }

        // POST: TrainingPlansView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TrainingPlan trainingPlan)
        {
            if (id != trainingPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingPlanExists(trainingPlan.Id))
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
            return View(trainingPlan);
        }

        // GET: TrainingPlansView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingPlan == null)
            {
                return NotFound();
            }

            return View(trainingPlan);
        }

        // POST: TrainingPlansView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingPlan = await _context.TrainingPlans.FindAsync(id);
            _context.TrainingPlans.Remove(trainingPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingPlanExists(int id)
        {
            return _context.TrainingPlans.Any(e => e.Id == id);
        }
    }
}
