using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetShapeDatabase;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryItemsController : ControllerBase
    {
        private readonly SetShapeContext _context;

        public HistoryItemsController(SetShapeContext context)
        {
            _context = context;
        }

        // GET: api/HistoryItems
        [HttpGet]
        [ProducesResponseType(typeof(List<HistoryItem>), StatusCodes.Status200OK)]
        public IEnumerable<HistoryItem> GetHistoryItems()
        {
            return _context.HistoryItems;
        }

        // GET: api/HistoryItems/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHistoryItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var historyItem = await _context.HistoryItems.FindAsync(id);

            if (historyItem == null)
            {
                return NotFound();
            }

            return Ok(historyItem);
        }

        // PUT: api/HistoryItems/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> PutHistoryItem([FromRoute] int id, [FromBody] HistoryItem historyItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historyItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(historyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HistoryItems
        [HttpPost]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostHistoryItem([FromBody] HistoryItem historyItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HistoryItems.Add(historyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoryItem", new { id = historyItem.Id }, historyItem);
        }

        // DELETE: api/HistoryItems/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHistoryItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var historyItem = await _context.HistoryItems.FindAsync(id);
            if (historyItem == null)
            {
                return NotFound();
            }

            _context.HistoryItems.Remove(historyItem);
            await _context.SaveChangesAsync();

            return Ok(historyItem);
        }

        private bool HistoryItemExists(int id)
        {
            return _context.HistoryItems.Any(e => e.Id == id);
        }
    }
}