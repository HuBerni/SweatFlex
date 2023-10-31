using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Services;

namespace SweatFlex.Maui.Services
{
    public class ProgressService

    {
        private readonly TrainingExerciseService _trainingExerciseService;
        private readonly WorkoutExerciseService _workoutExerciseService;
        private readonly IMapper _mapper;

        public ProgressService(TrainingExerciseService trainingExerciseService, WorkoutExerciseService workoutExerciseService, IMapper mapper)
        {
            _trainingExerciseService = trainingExerciseService;
            _workoutExerciseService = workoutExerciseService;
            _mapper = mapper;
        }

        public async Task<List<Progress>?> GetProgresses(string userId)
        {
            var progresses = new List<Progress>();
            var response = await _trainingExerciseService.GetTrainingExercisesAsync(userId);

            if (!response.IsSuccess)
            {
                return null;
            }

            var trainingExercisesGrouped = response.Result.GroupBy(x => x.SessionId).ToList();

            foreach (var item in trainingExercisesGrouped)
            {
                TimeSpan timeSpent = (item.Last().ExerciseExecuted - item.First().ExerciseExecuted) ?? TimeSpan.Zero;
                var workout = await GetWorkoutByWorkoutExercise(item.First().WorkoutExerciseId);

                var progress = new Progress()
                {
                    TotalWeight = item.Sum(x => x.Weight),
                    TimeSpent = timeSpent,
                    SessionId = item.First().SessionId ?? 0,
                    Workout = workout
                };

                progresses.Add(progress);
            }

            return progresses;
        }

        private async Task<Workout?> GetWorkoutByWorkoutExercise(int workoutExerciseId)
        {
            var response = await _workoutExerciseService.GetWorkoutExerciseByIdAsync(workoutExerciseId);

            if (!response.IsSuccess)
            {
                return null;
            }

            return _mapper.Map<Workout>(response.Result);
        }
    }
}
