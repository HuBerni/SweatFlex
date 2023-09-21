using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.DTOs;
using SweatFlexEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var tets = _context.TrainingExercises.Where(t => (t.UserId == userId && t.WorkoutExerciseId == workoutId)).ToList();
        }
        Task<TrainingExerciseDTO> GetTrainingExerciseAsync(int id);
        Task<ExerciseDTO> UpdateTrainingExerciseAsync(int id, TrainingExerciseUpdateDTO updateDTO);
        Task<bool> DeleteTrainingExerciseAsync(int id);
        Task<TrainingExerciseDTO> CreateTrainingExerciseAsync(TrainingExerciseCreateDTO createDTO);
    }
}
