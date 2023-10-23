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
        public async Task<TrainingExerciseDTO?> GetTrainingExerciseAsync(int id)
        {
            return Mapping.Mapper.Map<TrainingExerciseDTO>(await _context.TrainingExercises.Where(t => t.Id == id).FirstOrDefaultAsync());
        }
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
    }
}
