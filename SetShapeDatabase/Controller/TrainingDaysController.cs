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
    public class TrainingDaysController : ControllerBase
    {
        private readonly SetShapeContext _context;

        public TrainingDaysController(SetShapeContext context)
        {
            _context = context;
        }

        // GET: api/TrainingDays
        [HttpGet]
        public IEnumerable<TrainingDay> GetTrainingDays()
        {
            return _context.TrainingDays;
        }

        // GET: api/TrainingDays/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingDay = await _context.TrainingDays.FindAsync(id);

            if (trainingDay == null)
            {
                return NotFound();
            }

            return Ok(trainingDay);
        }

        // PUT: api/TrainingDays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingDay([FromRoute] int id, [FromBody] TrainingDay trainingDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingDayExists(id))
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

        // POST: api/TrainingDays
        [HttpPost]
        public async Task<IActionResult> PostTrainingDay([FromBody] TrainingDay trainingDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingDays.Add(trainingDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingDay", new { id = trainingDay.Id }, trainingDay);
        }

        // DELETE: api/TrainingDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingDay = await _context.TrainingDays.FindAsync(id);
            if (trainingDay == null)
            {
                return NotFound();
            }

            _context.TrainingDays.Remove(trainingDay);
            await _context.SaveChangesAsync();

            return Ok(trainingDay);
        }

        private bool TrainingDayExists(int id)
        {
            return _context.TrainingDays.Any(e => e.Id == id);
        }
    }
}