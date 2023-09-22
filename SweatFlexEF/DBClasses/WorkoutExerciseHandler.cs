using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs.Create;
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
    public class WorkoutExerciseHandler
    {
        SweatFlexContext _context;

        public WorkoutExerciseHandler(SweatFlexContext context)
        {
            _context = context;
        }

        public async Task<IList<WorkoutExerciseDTO>> GetWorkoutExercisesAsync(int workoutId)
        {
            var workoutExercises = await _context.WorkoutExercises.Where(w => w.WorkoutId == workoutId).ToListAsync();

            return workoutExercises.Select(Mapping.Mapper.Map<WorkoutExerciseDTO>).ToList();
        }
        public async Task<WorkoutExerciseDTO> GetWorkoutExerciseByIdAsnyc(int id) => 
            Mapping.Mapper.Map<WorkoutExerciseDTO>(await _context.WorkoutExercises.Where(w => w.Id == id).ToListAsync());
        
        public async Task<WorkoutExerciseDTO> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            var workoutExercise = Mapping.Mapper.Map<WorkoutExercise>(updateDTO);
            workoutExercise.Id = id;
            _context.WorkoutExercises.Update(workoutExercise);
            await _context.SaveChangesAsync();
            return Mapping.Mapper.Map<WorkoutExerciseDTO>(workoutExercise);
        }
        public async Task<bool> DeleteWorkoutExerciseAsync(int id)
        {
            var workoutExercise = await _context.WorkoutExercises.Where(w => w.Id == id).FirstOrDefaultAsync();
            _context.WorkoutExercises.Remove(workoutExercise);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<WorkoutExerciseDTO> CreateWorkoutExceriseAsync(WorkoutExerciseCreateDTO createDTO)
        {
            var workoutExercise = Mapping.Mapper.Map<WorkoutExercise>(createDTO);
            _context.WorkoutExercises.Add(workoutExercise);
            await _context.SaveChangesAsync();
            return Mapping.Mapper.Map<WorkoutExerciseDTO>(workoutExercise);                
        }
    }
}
