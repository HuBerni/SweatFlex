using AutoMapper;
using SweatFlex.Maui.Models;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Update;

namespace SweatFlex.Maui
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExerciseDTO, Exercise>().ReverseMap();
            CreateMap<WorkoutDTO, Workout>().ReverseMap();
            CreateMap<WorkoutExerciseDTO, WorkoutExercise>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<TrainingExerciseDTO, TrainingExercise>().ReverseMap();
            CreateMap<TrainingExercise, TrainingExerciseUpdateDTO>().ReverseMap();
        }
    }
}
