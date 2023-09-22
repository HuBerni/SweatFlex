using AutoMapper;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;

namespace SweatFlexEF
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ExerciseDTO, Exercise>().ReverseMap();
            CreateMap<ExerciseUpdateDTO, Exercise>().ReverseMap();
            CreateMap<ExerciseCreateDTO, Exercise>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, User>().ReverseMap();

            CreateMap<TrainingExerciseDTO, TrainingExercise>().ReverseMap();
            CreateMap<TrainingExerciseUpdateDTO, TrainingExercise>().ReverseMap();
            CreateMap<TrainingExerciseCreateDTO, TrainingExercise>().ReverseMap();

            CreateMap<WorkoutDTO, Workout>().ReverseMap();
            CreateMap<WorkoutUpdateDTO, Workout>().ReverseMap();
            CreateMap<WorkoutCreateDTO, Workout>().ReverseMap();
        }
    }
}
