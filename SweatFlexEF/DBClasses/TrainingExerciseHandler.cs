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

        public async Task<IList<TrainingExerciseDTO>> GetTrainingExerciesAsync(string? userId, int? workoutId = null)
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
        public async Task<TrainingExerciseDTO> GetTrainingExerciseAsync(int id)
        {
            return id == null ? Mapping.Mapper.Map<TrainingExerciseDTO>(_context.TrainingExercises.Where(t => t.Id == id).FirstOrDefault()) : null;
        }
        public async Task<TrainingExerciseDTO> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            if (id != null && updateDTO != null)
            {
                var trainingExercise = Mapping.Mapper.Map<TrainingExercise>(updateDTO);
                trainingExercise.Id = id;
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
            if (id != null)
            {
                var trainingExercise = _context.TrainingExercises.Where(t => t.Id == id).FirstOrDefault();
                _context.TrainingExercises.Remove(trainingExercise);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<TrainingExerciseDTO> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO)
        {
            if (createDTO != null)
            {
                var trainingExercise = Mapping.Mapper.Map<TrainingExercise>(createDTO);
                await _context.TrainingExercises.AddAsync(trainingExercise);
                await _context.SaveChangesAsync();

                return Mapping.Mapper.Map<TrainingExerciseDTO>(trainingExercise);
            }
            else
            {
                return null;
            }
        }
    }
}
