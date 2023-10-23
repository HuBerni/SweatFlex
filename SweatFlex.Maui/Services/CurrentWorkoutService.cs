using AutoMapper;
using SweatFlex.Maui.Models;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;

namespace SweatFlex.Maui.Services
{
    public class CurrentWorkoutService
    {
        private readonly TrainingExerciseService _trainingExerciseService;
        private readonly WorkoutExerciseService _workoutExerciseService;
        private readonly IMapper _mapper;

        public CurrentWorkoutService(TrainingExerciseService trainingExerciseService, WorkoutExerciseService workoutExerciseService, IMapper mapper)
        {
            _trainingExerciseService = trainingExerciseService;
            _workoutExerciseService = workoutExerciseService;
            _mapper = mapper;
        }


        public async Task<List<TrainingExercise>> CreateTrainingExercisesForWorkout(int workoutId)
        {
            var trainingExercises = new List<TrainingExercise>();

            var workoutExercisesResponse = await _workoutExerciseService.GetWorkoutExercisesAsync(workoutId);

            if (!workoutExercisesResponse.IsSuccess)
            {
                //error handling
            }

            foreach (var item in workoutExercisesResponse.Result)
            {
                var trainingExerciseCreateDto = new TrainingExerciseCreateDTO()
                {
                    ExerciseId = item.Exercise.Id,
                    UserId = "TESTI",
                    WorkoutExerciseId = item.Id,
                };

                var trainingExerciseResponse = await _trainingExerciseService.CreateTrainingExerciseAsync(trainingExerciseCreateDto);

                if (!trainingExerciseResponse.IsSuccess)
                {
                    //error handling
                    continue;
                }

                trainingExercises.Add(_mapper.Map<TrainingExercise>(trainingExerciseResponse.Result));
            }
            
            return trainingExercises;
        }

        public async Task<TrainingExercise> UpdateTrainingExerciseAsync(TrainingExercise trainingExercise)
        {
            trainingExercise.ExerciseExecuted = DateTime.Now;
            var trainingExerciseUpdateDto = _mapper.Map<TrainingExerciseUpdateDTO>(trainingExercise);

            var trainingExerciseResponse = await _trainingExerciseService.UpdateTrainingExerciseAsync(trainingExercise.Id, trainingExerciseUpdateDto);

            if (!trainingExerciseResponse.IsSuccess)
            {
                //error handling
            }

            return _mapper.Map<TrainingExercise>(trainingExerciseResponse.Result);
        }
    }
}
