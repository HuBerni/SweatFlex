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

        /// <summary>
        /// Reads a List of WorkoutExercises from the database, depending on the workoutId
        /// </summary>
        /// <param name="workoutId">WorkoutId</param>
        /// <returns></returns>
        public async Task<IList<WorkoutExerciseDTO>> GetWorkoutExercisesAsync(int workoutId)
        {
            var workoutExercises = await _context.WorkoutExercises.Where(w => w.WorkoutId == workoutId).ToListAsync();

            return workoutExercises.Select(Mapping.Mapper.Map<WorkoutExerciseDTO>).ToList();
        }

        /// <summary>
        /// Reads 1 WorkoutExercise from the Database, depending on the WorkoutExerciseId
        /// </summary>
        /// <param name="id">WorkoutExerciseId</param>
        /// <returns></returns>
        public async Task<WorkoutExerciseDTO?> GetWorkoutExerciseByIdAsnyc(int id)
        {
            var trainingExercise = await _context.WorkoutExercises.Where(w => w.Id == id).FirstOrDefaultAsync();
            
            return Mapping.Mapper.Map<WorkoutExerciseDTO>(trainingExercise);
        }
        
        /// <summary>
        /// Updates 1 WorkoutExercise in the Database, depending on the WorkoutExerciseId
        /// </summary>
        /// <param name="id">WorkoutExerciseId</param>
        /// <param name="updateDTO">Data for update</param>
        /// <returns></returns>
        public async Task<WorkoutExerciseDTO> UpdateWorkoutExerciseAsync(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            var workoutExercise = Mapping.Mapper.Map<WorkoutExercise>(updateDTO);
            workoutExercise.Id = id;
            _context.WorkoutExercises.Update(workoutExercise);
            await _context.SaveChangesAsync();
            return Mapping.Mapper.Map<WorkoutExerciseDTO>(workoutExercise);
        }

        /// <summary>
        /// Deletes 1 WorkoutExercise from the Database, depending on the WorkoutExerciseId
        /// </summary>
        /// <param name="id">WorkoutExerciseId</param>
        /// <returns></returns>
        public async Task<bool> DeleteWorkoutExerciseAsync(int id)
        {
            var workoutExercise = await _context.WorkoutExercises.Where(w => w.Id == id).FirstOrDefaultAsync();
            _context.WorkoutExercises.Remove(workoutExercise);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Creates 1 new WorkoutExericse in the Database
        /// </summary>
        /// <param name="createDTO">Data for creation</param>
        /// <returns></returns>
        public async Task<WorkoutExerciseDTO> CreateWorkoutExceriseAsync(WorkoutExerciseCreateDTO createDTO)
        {
            var workoutExercise = Mapping.Mapper.Map<WorkoutExercise>(createDTO);
            _context.WorkoutExercises.Add(workoutExercise);
            await _context.SaveChangesAsync();
            return Mapping.Mapper.Map<WorkoutExerciseDTO>(workoutExercise);                
        }
    }
}
