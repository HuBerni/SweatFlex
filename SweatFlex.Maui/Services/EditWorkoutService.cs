using AutoMapper;
using SweatFlex.Maui.Models;
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
        }

        public async Task AddWorkoutExercise(int workoutId, int exerciseId, int sets)
        {
            var workoutExercise = new WorkoutExerciseCreateDTO()
            {
                ExerciseId = exerciseId,
                WorkoutId = workoutId
            };

            _workoutExerciseCreateDtos.AddRange(Enumerable.Repeat(workoutExercise, sets));
            
            ExerciseSets.Add(new ExerciseSet()
            {
                Exercise = _mapper.Map<Exercise>(await _exerciseService.GetExerciseAsync(exerciseId)),
                Sets = sets
            });

            AddIndexesToWorkoutExercises();
        }

        public async Task RemoveWorkoutExercise(int workoutId, int exerciseId)
        {

        }

        public async Task SaveWorkoutExercises()
        {
            foreach (var item in _workoutExerciseCreateDtos)
            {
                await _workoutExerciseService.CreateWorkoutExerciseAsync(item);
            }
        }

        private void AddIndexesToWorkoutExercises()
        {
            for (int i = 1; i <= _workoutExerciseCreateDtos.Count; i++)
            {
                _workoutExerciseCreateDtos[i - 1].Index = i;
            }
        }
    }
}
