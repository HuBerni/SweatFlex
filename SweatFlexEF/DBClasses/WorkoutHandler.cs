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

        public async Task<IList<WorkoutDTO>> GetWorkoutsAsync(string? userId = null)
        {
            List<Workout> workouts = new();

            if(userId != null)
            {
                var nonCustomerIds = _context.Users.Where(u => u.Role != 1).Select(s => s.Id).ToList();
                workouts = await _context.Workouts.Where(e => nonCustomerIds.Any(i => i == e.Creator)).ToListAsync();
            }
            else
            {
                workouts = await _context.Workouts.Where(w => w.Creator == userId).ToListAsync();
            }

            return workouts.Select(Mapping.Mapper.Map<WorkoutDTO>).ToList();
        }
        public async Task<WorkoutDTO> GetWorkoutByIdAsync(int id)
        {
            var workout = await _context.Workouts.Where(w => w.Id == id).FirstOrDefaultAsync();
            return Mapping.Mapper.Map<WorkoutDTO>(workout);
     
        }
        public async Task<WorkoutDTO> UpdateWorkoutsAsynct(int id, WorkoutUpdateDTO updateDTO)
        {
            var workout = Mapping.Mapper.Map<Workout>(updateDTO);
            workout.Id = id;
            _context.Workouts.Update(workout);

            return Mapping.Mapper.Map<WorkoutDTO>(await _context.Workouts.Where(w => w.Id == id).FirstOrDefaultAsync());
        }
        public async Task<bool> DeleteWorkoutAsync(int id)
        {
            var workout = await _context.Workouts.Where(w => w.Id == id).FirstOrDefaultAsync();

            _context.Remove(workout);

            return true;
        }
        public async Task<WorkoutDTO> CreateWorkoutsAsynct(WorkoutCreateDTO creatDTO)
        {
            var workout = Mapping.Mapper.Map<Workout>(creatDTO);
            _context.Workouts.Add(workout);

            return Mapping.Mapper.Map<WorkoutDTO>(workout);
        }

    }
}
