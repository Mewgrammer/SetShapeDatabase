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
    public class TrainingPlansController : ControllerBase
    {
        private readonly SetShapeContext _context;

        public TrainingPlansController(SetShapeContext context)
        {
            _context = context;
        }

        // GET: api/TrainingPlans
        [HttpGet]
        public IEnumerable<TrainingPlan> GetTrainingPlans()
        {
            return _context.TrainingPlans;
        }

        // GET: api/TrainingPlans/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTrainingPlan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingPlan = await _context.TrainingPlans.FindAsync(id);

            if (trainingPlan == null)
            {
                return NotFound();
            }

            return Ok(trainingPlan);
        }

        // PUT: api/TrainingPlans/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> PutTrainingPlan([FromRoute] int id, [FromBody] TrainingPlan trainingPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingPlanExists(id))
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

        // POST: api/TrainingPlans
        [HttpPost]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTrainingPlan([FromBody] TrainingPlan trainingPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingPlans.Add(trainingPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingPlan", new { id = trainingPlan.Id }, trainingPlan);
        }

        // DELETE: api/TrainingPlans/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrainingPlan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingPlan = await _context.TrainingPlans.FindAsync(id);
            if (trainingPlan == null)
            {
                return NotFound();
            }

            _context.TrainingPlans.Remove(trainingPlan);
            await _context.SaveChangesAsync();

            return Ok(trainingPlan);
        }

        private bool TrainingPlanExists(int id)
        {
            return _context.TrainingPlans.Any(e => e.Id == id);
        }
    }
}