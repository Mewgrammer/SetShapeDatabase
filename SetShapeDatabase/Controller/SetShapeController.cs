using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SetShapeDatabase.Controller.Forms;
using SetShapeDatabase.Entities;

namespace SetShapeDatabase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetShapeController : ControllerBase
    {

        private readonly SetShapeContext _context;

        public SetShapeController(SetShapeContext context)
        {
            _context = context;
        }


        [HttpGet("/user/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFullUser([FromRoute]int id)
        {
            var user = await GetUserAsync(id);
            _context.Dispose();
            return Ok(user);
        }

        [HttpPost("/training")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddTrainingToUser([FromBody] UserTrainingForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = await _context.Users.SingleOrDefaultAsync(d => d.Id == data.UserId);
                if (user == null)
                {
                    return NotFound(data.UserId);
                }

                var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(t => t.Id == data.TrainingPlan.Id);

                if (trainingPlan == null)
                {
                    if (data.TrainingPlan.Id != 0)
                    {
                        return NotFound(data.TrainingPlan);
                    }
                }
                user.Trainings.Add(data.TrainingPlan);
                await _context.SaveChangesAsync();
                var addedTraining = user.Trainings.LastOrDefault();
                var workoutDays = new List<TrainingDayWorkout>();
                foreach (var day in addedTraining?.Days)
                {
                    foreach (var workout in _context.Workouts.Where(w => day.Workouts.Any(x => x.Id == w.Id)))
                    {
                        workoutDays.Add(new TrainingDayWorkout { TrainingDay = day, TrainingDayId = day.Id, Workout = workout, WorkoutId = workout.Id});
                    }
                }
                _context.TrainingDayWorkouts.AddRange(workoutDays);
                await _context.SaveChangesAsync();
                user.PrepareSerialize(_context.Workouts.ToList());
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("/training")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SetActiveTrainingPlan([FromBody] UserTrainingForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(p => p.Id == data.TrainingPlan.Id);
                if (trainingPlan == null)
                {
                    return NotFound(data.TrainingPlan);
                }

                var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == data.UserId);
                if (user == null)
                {
                    return NotFound(data.UserId);
                }
                user.CurrentTrainingPlan = trainingPlan;
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpDelete("/training")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTrainingFromUser([FromBody] UserTrainingForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = await GetUserAsync(data.UserId);
                if (user == null)
                {
                    return NotFound(data.UserId);
                }

                var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(t => t.Id == data.TrainingPlan.Id);
                if (trainingPlan == null || user.Trainings.All(t => t.Id != trainingPlan.Id))
                { 
                    return NotFound(data.TrainingPlan);
                }
                user.Trainings.Remove(trainingPlan);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }



        [HttpPost("/day")]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddTrainingDayToTrainingPlan([FromBody] TrainingPlanDayForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(p => p.Id == data.TrainingPlanId);
                if (trainingPlan == null)
                {
                    return NotFound(data.TrainingPlanId);
                }

                var trainingDay = await _context.TrainingDays.SingleOrDefaultAsync(t => t.Id == data.Day.Id);
                if (trainingDay == null)
                {
                    if (data.Day.Id != 0)
                    {
                        return NotFound(data.Day);
                    }
                    var workoutDays = new List<TrainingDayWorkout>();
                    foreach (var workout in _context.Workouts.Where(w => data.Day.Workouts.Any(x => x.Id == w.Id)))
                    {
                        workoutDays.Add(new TrainingDayWorkout { TrainingDay = data.Day, TrainingDayId = data.Day.Id, Workout = workout, WorkoutId = workout.Id });
                    }
                    data.Day.TrainingDayWorkouts = workoutDays;
                    var result = _context.TrainingDays.Add(data.Day);
                    trainingDay = result.Entity;
                }
                trainingPlan.Days.Add(trainingDay);
                await _context.SaveChangesAsync();
                return Ok(trainingPlan);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("/day")]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTrainingDayFromTrainingPlan([FromBody] TrainingPlanDayForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(p => p.Id == data.TrainingPlanId);
                if (trainingPlan == null)
                {
                    return NotFound(data.TrainingPlanId);
                }

                var trainingDay = await _context.TrainingDays.SingleOrDefaultAsync(t => t.Id == data.Day.Id);
                if (trainingDay == null)
                {
                    return NotFound(data.Day);
                }
                trainingPlan.Days.Remove(trainingDay);
                await _context.SaveChangesAsync();
                return Ok(trainingPlan);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("/workout")]
        [ProducesResponseType(typeof(TrainingDayWorkout), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Workout), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddWorkoutToDay([FromBody] WorkoutDayForm data) 
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return BadRequest();
                }

                var day = await _context.TrainingDays.SingleOrDefaultAsync(d => d.Id == data.DayId);
                var workout = await _context.Workouts.SingleOrDefaultAsync(w => w.Id == data.WorkoutId);
                var workoutDay = new TrainingDayWorkout { TrainingDay = day, TrainingDayId = day.Id, Workout = workout, WorkoutId = workout.Id};
                day.TrainingDayWorkouts.Add(workoutDay);
                await _context.SaveChangesAsync();
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("/workout")]
        [ProducesResponseType(typeof(TrainingDayWorkout), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Workout), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveWorkoutFromDay([FromBody] WorkoutDayForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var day = await _context.TrainingDays.Include(d => d.TrainingDayWorkouts).SingleOrDefaultAsync(d => d.Id == data.DayId);
                var dayWorkout = day.TrainingDayWorkouts.FirstOrDefault(w => w.WorkoutId == data.WorkoutId);
                day.TrainingDayWorkouts.Remove(dayWorkout);
                await _context.SaveChangesAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("/history")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddHistoryItemToDay([FromBody] DayHistoryItemForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var day = await _context.TrainingDays.SingleOrDefaultAsync(d => d.Id == data.DayId);
                if (day == null)
                {
                    return NotFound(data.DayId);
                }

                var item = await _context.HistoryItems.SingleOrDefaultAsync(i => i.Id == data.Item.Id);
                if (item == null)
                {
                    if (data.Item.Id != 0)
                    {
                        return NotFound(data.Item);
                    }
                    data.Item.Workout = await _context.Workouts.SingleOrDefaultAsync(w => w.Id == data.Item.Workout.Id);
                    var result = await _context.HistoryItems.AddAsync(data.Item);
                    item = result.Entity;
                }
                day.History.Add(item);
                await _context.SaveChangesAsync();
                return Ok(day);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("/history")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveHistoryItemFromDay([FromBody] DayHistoryItemForm data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var day = await _context.TrainingDays.SingleOrDefaultAsync(d => d.Id == data.DayId);
                if (day == null)
                {
                    return NotFound(data.DayId);
                }

                var item = await _context.HistoryItems.SingleOrDefaultAsync(i => i.Id == data.Item.Id);
                if (item == null)
                {
                    return NotFound(data.Item);
                }
                day.History.Remove(item);
                await _context.SaveChangesAsync();
                return Ok(day);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        private async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Trainings).ThenInclude(t => t.Days).ThenInclude(d => d.TrainingDayWorkouts)
                .Include(u => u.Trainings).ThenInclude(t => t.Days).ThenInclude(d => d.History)
                .SingleOrDefaultAsync(u => u.Id == id);
            user.PrepareSerialize(_context.Workouts.ToList());
            return user;

        }
    }
}
