using Microsoft.EntityFrameworkCore;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;

namespace SweatFlexEF.DBClasses
{
    public class TrainingExerciseHandler
    {
        SweatFlexContext _context;

        public TrainingExerciseHandler(SweatFlexContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Reads a List of TraingingExercises from the Database, depending on Params
        /// </summary>
        /// <param name="userId">CreaterId of the TrainingExercises</param>
        /// <param name="workoutId">WorkoutId of the TrainingExercises. If null, gets all</param>
        /// <returns></returns>
        public async Task<IList<TrainingExerciseDTO>?> GetTrainingExerciesAsync(string? userId, int? workoutId = null)
        {
            List<TrainingExercise> trainingExercises = new();

            if (userId == null)
            {
                return null;
            }
            else if (workoutId == null)
            {
                trainingExercises = await _context.TrainingExercises.Where(t => t.UserId == userId).ToListAsync();
            }
            else
            {
                trainingExercises = await _context.TrainingExercises.Where(t => (t.UserId == userId && t.WorkoutExerciseId == workoutId)).ToListAsync();
            }

            return trainingExercises.Select(Mapping.Mapper.Map<TrainingExerciseDTO>).ToList();

        }

        /// <summary>
        /// Reads 1 TrainingExercise, depending on the TrainingExerciseId
        /// </summary>
        /// <param name="id">TrainingExerciseId</param>
        /// <returns></returns>
        public async Task<TrainingExerciseDTO?> GetTrainingExerciseAsync(int id)
        {
            return Mapping.Mapper.Map<TrainingExerciseDTO>(await _context.TrainingExercises.Where(t => t.Id == id).FirstOrDefaultAsync());
        }

        /// <summary>
        /// Updates 1 TrainingExercise depending on the TrainingExerciseId
        /// </summary>
        /// <param name="id">TrainingExerciseId</param>
        /// <param name="updateDTO">Data for Update</param>
        /// <returns></returns>
        public async Task<TrainingExerciseDTO?> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            if (updateDTO != null)
            {
                var trainingExercise = await _context.TrainingExercises.Where(t => t.Id == id).FirstOrDefaultAsync();

                if (trainingExercise == null)
                {
                    return null;
                }

                trainingExercise.Weight = updateDTO.Weight;
                trainingExercise.Reputations = updateDTO.Reputations; 
                trainingExercise.TimeInSec = updateDTO.TimeInSec;
                trainingExercise.ExerciseExecuted = updateDTO.ExerciseExecuted;

                _context.TrainingExercises.Update(trainingExercise);
                await _context.SaveChangesAsync();
                return Mapping.Mapper.Map<TrainingExerciseDTO>(trainingExercise);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes 1 TrainingExercise from the Database, depending on the TrainingExerciseId
        /// </summary>
        /// <param name="id">TrainingExerciseId</param>
        /// <returns></returns>
        public async Task<bool> DeleteTrainingExerciseAsync(int id)
        {
            var trainingExercise = await _context.TrainingExercises.Where(t => t.Id == id).FirstOrDefaultAsync();

            if(trainingExercise == null)
            {
                return false;
            }

            _context.TrainingExercises.Remove(trainingExercise);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Creates a List of TrainingExercises in the Database
        /// </summary>
        /// <param name="createDTO">List of data for creation</param>
        /// <returns></returns>
        public async Task<IList<TrainingExerciseDTO>?> CreateTrainingExerciseAsync(IList<TrainingExerciseCreateDTO> createDTO)
        {
            if (createDTO != null)
            {
                var result = _context.fn_getNextSessionId(createDTO[0].UserId).FirstOrDefault();
                foreach(var create in createDTO)
                {
                    create.SessionId = result.nextSessionId;
                }
                
                var trainingExercise = createDTO.Select(Mapping.Mapper.Map<TrainingExercise>).ToList();
                await _context.TrainingExercises.AddRangeAsync(trainingExercise);
                await _context.SaveChangesAsync();

                return trainingExercise.Select(Mapping.Mapper.Map<TrainingExerciseDTO>).ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Reads a TrainingExercise from the Database based on a sessioId
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<TrainingExerciseDTO> GetTrainingExerciseBySessionIdAsync(int sessionId)
        {
            return Mapping.Mapper.Map<TrainingExerciseDTO>(_context.TrainingExercises.Where(t => t.SessionId == sessionId).FirstOrDefault());            
        }
    }
}
