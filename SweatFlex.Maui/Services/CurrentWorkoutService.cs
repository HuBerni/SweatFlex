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


        public async Task<List<TrainingExercise>> CreateTrainingExercisesForWorkout(int workoutId, string userId)
        {
            var trainingExerciseCreateDTOs = new List<TrainingExerciseCreateDTO>();            

            var workoutExercisesResponse = await _workoutExerciseService.GetWorkoutExercisesAsync(workoutId);

            if (!workoutExercisesResponse.IsSuccess)
            {
                //error handling
                return null;
            }

            foreach (var item in workoutExercisesResponse.Result)
            {
                var trainingExerciseCreateDto = new TrainingExerciseCreateDTO()
                {
                    ExerciseId = item.Exercise.Id,
                    UserId = userId,
                    WorkoutExerciseId = item.Id,
                };

                trainingExerciseCreateDTOs.Add(trainingExerciseCreateDto);
            }

            var trainingExerciseResponse = await _trainingExerciseService.CreateTrainingExerciseAsync(trainingExerciseCreateDTOs);

            if (!trainingExerciseResponse.IsSuccess)
            {
                //error handling
                return null;
            }

            return trainingExerciseResponse.Result.Select(_mapper.Map<TrainingExercise>).ToList();
        }

        public async Task<TrainingExercise> UpdateTrainingExerciseAsync(TrainingExercise trainingExercise)
        {
            trainingExercise.ExerciseExecuted = DateTime.Now;
            var trainingExerciseUpdateDto = _mapper.Map<TrainingExerciseUpdateDTO>(trainingExercise);

            var trainingExerciseResponse = await _trainingExerciseService.UpdateTrainingExerciseAsync(trainingExercise.Id, trainingExerciseUpdateDto);

            if (!trainingExerciseResponse.IsSuccess)
            {
                //error handling
                return null;
            }

            return _mapper.Map<TrainingExercise>(trainingExerciseResponse.Result);
        }
    }
}
