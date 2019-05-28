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
    public class HistoryViewController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SetShapeContext _context;

        public HistoryViewController(SetShapeContext context)
        {
            _context = context;
        }

        // GET: HistoryView
        public async Task<IActionResult> Index()
        {
            return View(await _context.HistoryItems.ToListAsync());
        }

        // GET: HistoryView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyItem = await _context.HistoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historyItem == null)
            {
                return NotFound();
            }

            return View(historyItem);
        }

        // GET: HistoryView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistoryView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Sets,Repetitions,Weight")] HistoryItem historyItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historyItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historyItem);
        }

        // GET: HistoryView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyItem = await _context.HistoryItems.FindAsync(id);
            if (historyItem == null)
            {
                return NotFound();
            }
            return View(historyItem);
        }

        // POST: HistoryView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Sets,Repetitions,Weight")] HistoryItem historyItem)
        {
            if (id != historyItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historyItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoryItemExists(historyItem.Id))
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
            return View(historyItem);
        }

        // GET: HistoryView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyItem = await _context.HistoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historyItem == null)
            {
                return NotFound();
            }

            return View(historyItem);
        }

        // POST: HistoryView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historyItem = await _context.HistoryItems.FindAsync(id);
            _context.HistoryItems.Remove(historyItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryItemExists(int id)
        {
            return _context.HistoryItems.Any(e => e.Id == id);
        }
    }
}
