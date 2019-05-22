﻿using System;
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
            return Ok(user);
        }

        [HttpPost("/training")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddTrainingToUser([FromBody] UserTrainingForm data)
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
                var result = await _context.TrainingPlans.AddAsync(data.TrainingPlan);
                trainingPlan = result.Entity;
            }
            user.Trainings.Add(trainingPlan);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("/training")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTrainingFromUser([FromBody] UserTrainingForm data)
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

        [HttpPost("/day")]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddTrainingDayToTrainingPlan([FromBody] TrainingPlanDayForm data)
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
                var result = await _context.TrainingDays.AddAsync(data.Day);
                trainingDay = result.Entity;
            }
            trainingPlan.Days.Add(trainingDay);
            await _context.SaveChangesAsync();
            return Ok(trainingPlan);
        }

        [HttpDelete("/day")]
        [ProducesResponseType(typeof(TrainingPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTrainingDayFromTrainingPlan([FromBody] TrainingPlanDayForm data)
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

        [HttpPost("/workout")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Workout), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddWorkoutToDay([FromBody] WorkoutDayForm data) 
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

            var workout = await _context.Workouts.SingleOrDefaultAsync(w => w.Id == data.Workout.Id);
            if (workout == null)
            {
                if (data.Workout.Id != 0)
                {
                    return NotFound(data.Workout);
                }
                var result = await _context.Workouts.AddAsync(data.Workout);
                workout = result.Entity;
            }
            day.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return Ok(day);

        }

        [HttpDelete("/workout")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Workout), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveWorkoutFromDay([FromBody] WorkoutDayForm data)
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

            var workout = await _context.Workouts.SingleOrDefaultAsync(w => w.Id == data.Workout.Id);
            if (workout == null)
            {
                return NotFound(data.Workout);
            }
            day.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return Ok(day);
        }

        [HttpPost("/history")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddHistoryItemToDay([FromBody] DayHistoryItemForm data)
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
                var result = await _context.HistoryItems.AddAsync(data.Item);
                item = result.Entity;
            }
            day.History.Add(item);
            await _context.SaveChangesAsync();
            return Ok(day);
        }

        [HttpDelete("/history")]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TrainingDay), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HistoryItem), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveHistoryItemFromDay([FromBody] DayHistoryItemForm data)
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

        private async Task<User> GetUserAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Trainings).ThenInclude(t => t.Days).ThenInclude(d => d.Workouts)
                .Include(u => u.Trainings).ThenInclude(t => t.Days).ThenInclude(d => d.History)
                .SingleOrDefaultAsync(u => u.Id == id);

        }
    }
}
