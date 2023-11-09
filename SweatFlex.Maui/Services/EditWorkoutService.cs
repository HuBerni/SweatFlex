using AutoMapper;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Views;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs.Create;

namespace SweatFlex.Maui.Services
{
    public class EditWorkoutService
    {
        private readonly IMapper _mapper;
        private readonly ExerciseService _exerciseService;
        private readonly WorkoutExerciseService _workoutExerciseService;
        private List<Exercise> _exercises;
        private List<WorkoutExerciseCreateDTO> _workoutExerciseCreateDtos;

        public List<ExerciseSet> ExerciseSets { get; set; } = new();


        public EditWorkoutService(IMapper mapper, ExerciseService exerciseService, WorkoutExerciseService workoutExerciseService)
        {
            _workoutExerciseCreateDtos = new();
            _mapper = mapper;
            _exerciseService = exerciseService;
            _workoutExerciseService = workoutExerciseService;

            SetExercises();
        }

        /// <summary>
        /// Sets the exercises for the current user
        /// </summary>
        /// <returns></returns>
        private async Task SetExercises()
        {
            var response = await _exerciseService.GetExercisesAsync();
            
            if (response.IsSuccess)
            {
                _exercises = response.Result.Select(x => _mapper.Map<Exercise>(x)).ToList();
            }
        }

        /// <summary>
        /// Adds an exercise and the sets to the workout
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="exerciseId"></param>
        /// <param name="sets"></param>
        /// <returns></returns>
        public async Task AddWorkoutExercise(int workoutId, int exerciseId, int sets)
        {
            for (int i = 0; i < sets; i++)
            {
                _workoutExerciseCreateDtos.Add(new WorkoutExerciseCreateDTO()
                {
                    ExerciseId = exerciseId,
                    WorkoutId = workoutId
                });
            }
            
            ExerciseSets.Add(new ExerciseSet()
            {
                Exercise = _exercises.FirstOrDefault(x => x.Id == exerciseId),
                Sets = sets
            });

            AddIndexesToWorkoutExercises();
        }

        /// <summary>
        /// Removes an exercise from the workout
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <returns></returns>
        public async Task RemoveWorkoutExercise(int exerciseId)
        {
            _workoutExerciseCreateDtos.RemoveAll(x => x.ExerciseId == exerciseId);
            ExerciseSets.RemoveAll(x => x.Exercise.Id == exerciseId);
            AddIndexesToWorkoutExercises();
        }

        /// <summary>
        /// Saves the workoutexercises to the database
        /// </summary>
        /// <returns></returns>
        public async Task SaveWorkoutExercises()
        {
            foreach (var item in _workoutExerciseCreateDtos)
            {
                await _workoutExerciseService.CreateWorkoutExerciseAsync(item);
            }
        }

        /// <summary>
        /// settting the indexes for the workoutexercises
        /// </summary>
        private void AddIndexesToWorkoutExercises()
        {
            int index = 1;
            foreach (var item in _workoutExerciseCreateDtos)
            {
                item.Index = index;
                index++;
            }
        }
    }
}
