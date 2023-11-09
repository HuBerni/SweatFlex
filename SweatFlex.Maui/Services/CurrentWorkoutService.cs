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

        /// <summary>
        /// Creates trainingexercises for a workout and returns them
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<TrainingExercise>> CreateTrainingExercisesForWorkout(int workoutId, string userId)
        {
            var trainingExerciseCreateDTOs = new List<TrainingExerciseCreateDTO>();            

            var workoutExercisesResponse = await _workoutExerciseService.GetWorkoutExercisesAsync(workoutId);

            if (!workoutExercisesResponse.IsSuccess)
            {
                //error handling
                return null;
            }

            var workoutExercises = workoutExercisesResponse.Result!.OrderBy(x => x.Index).ToList();

            foreach (var item in workoutExercises)
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

            for (int i = 0; i < workoutExercises.Count(); i++)
            {
                trainingExerciseResponse.Result[i].Exercise = workoutExercises[i].Exercise;
            }

            return trainingExerciseResponse.Result.Select(_mapper.Map<TrainingExercise>).ToList();
        }

        /// <summary>
        /// Gets the training exercise setting the new values and returns it
        /// </summary>
        /// <param name="trainingExercise"></param>
        /// <returns></returns>
        public async Task<TrainingExercise> UpdateTrainingExerciseAsync(TrainingExercise trainingExercise)
        {
            if (trainingExercise.ExerciseExecuted is null)
            {
                trainingExercise.ExerciseExecuted = DateTime.Now;
            }

            var trainingExerciseUpdateDto = _mapper.Map<TrainingExerciseUpdateDTO>(trainingExercise);

            var trainingExerciseResponse = await _trainingExerciseService.UpdateTrainingExerciseAsync(trainingExercise.Id, trainingExerciseUpdateDto);

            if (!trainingExerciseResponse.IsSuccess)
            {
                //error handling
                return null;
            }

            return _mapper.Map<TrainingExercise>(trainingExerciseResponse.Result);
        }

        /// <summary>
        /// Updates the training exercise and returns it
        /// </summary>
        /// <param name="trainingExercise"></param>
        /// <returns></returns>
        public async Task<TrainingExercise> UpdateTrainingExerciseAsync(TrainingExerciseLocal trainingExercise)
        {
            if (trainingExercise.ExerciseExecuted is null)
            {
                trainingExercise.ExerciseExecuted = DateTime.Now;
            }

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
