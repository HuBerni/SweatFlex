using AutoMapper;
using SweatFlex.Maui.Models;
using SweatFlexData.DTOs;

namespace SweatFlex.Maui
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExerciseDTO, Exercise>().ReverseMap();
            CreateMap<WorkoutDTO, Workout>().ReverseMap();
            CreateMap<WorkoutExerciseDTO, WorkoutExercise>().ReverseMap();
        }
    }
}
