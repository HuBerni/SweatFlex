using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.DTOs;
using SweatFlexEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SweatFlexEF.DBClasses
{
    public class WorkoutHandler
    {
        SweatFlexContext _context;

        public WorkoutHandler(SweatFlexContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Reads a List of Workouts from the Database, depending on the CreatorId. If CreatorId == null, reads all Workouts that are not created by a Customer.
        /// </summary>
        /// <param name="userId">CreatorId</param>
        /// <returns></returns>
        public async Task<IList<WorkoutDTO>> GetWorkoutsAsync(string? userId = null)
        {
            List<Workout> workouts = new();

            if(userId is null)
            {
                var nonCustomerIds = _context.Users.Where(u => u.Role == 3).Select(s => s.Id).ToList();
                workouts = await _context.Workouts.Where(e => nonCustomerIds.Any(i => i == e.Creator)).ToListAsync();
            }
            else
            {
                workouts = await _context.Workouts.Where(w => w.Creator == userId).ToListAsync();
            }

            return workouts.Select(Mapping.Mapper.Map<WorkoutDTO>).ToList();
        }

        /// <summary>
        /// Reads 1 Workout from the Database, depending on the WorkoutId
        /// </summary>
        /// <param name="id">WorkoutId</param>
        /// <returns></returns>
        public async Task<WorkoutDTO> GetWorkoutByIdAsync(int id)
        {
            var workout = await _context.Workouts.Where(w => w.Id == id).FirstOrDefaultAsync();
            return Mapping.Mapper.Map<WorkoutDTO>(workout);     
        }

        /// <summary>
        /// Updates 1 Workout in the Database, depending on the WorkoutId
        /// </summary>
        /// <param name="id">WorkoutId</param>
        /// <param name="updateDTO">Data for update</param>
        /// <returns></returns>
        public async Task<WorkoutDTO?> UpdateWorkoutsAsynct(int id, WorkoutUpdateDTO updateDTO)
        {
            var workout = _context.Workouts.Where(w => w.Id == id).FirstOrDefault();
            
            if (workout == null)
            {
                return null;
            }

            workout.Name = updateDTO.Name;

            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();

            return Mapping.Mapper.Map<WorkoutDTO>(await _context.Workouts.Where(w => w.Id == id).FirstOrDefaultAsync());
        }

        /// <summary>
        /// Deletes 1 Workout from the Database, depending on the WorkoutId
        /// </summary>
        /// <param name="id">WorkoutId</param>
        /// <returns></returns>
        public async Task<bool> DeleteWorkoutAsync(int id)
        {
            var workout = await _context.Workouts.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (workout == null)
            {
                return false;
            }

            _context.Remove(workout);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Creates 1 new Workout in the Database
        /// </summary>
        /// <param name="creatDTO">Data for creation</param>
        /// <returns></returns>
        public async Task<WorkoutDTO> CreateWorkoutsAsynct(WorkoutCreateDTO creatDTO)
        {
            var workout = Mapping.Mapper.Map<Workout>(creatDTO);
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return Mapping.Mapper.Map<WorkoutDTO>(workout);
        }

    }
}
