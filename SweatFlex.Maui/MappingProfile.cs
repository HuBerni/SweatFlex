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
            CreateMap<Musclegroup, MusclegroupDTO>().ReverseMap();
            CreateMap<ExerciseType, TypeDTO>().ReverseMap();
            CreateMap<Equipment, EquipmentDTO>().ReverseMap();
            CreateMap<TrainingExercise, TrainingExerciseLocal>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(x => x.ExerciseId, opt => opt.MapFrom(src => src.Exercise.Id))
                .ReverseMap();
            CreateMap<TrainingExerciseUpdateDTO, TrainingExerciseLocal>().ReverseMap();

        }
    }
}
