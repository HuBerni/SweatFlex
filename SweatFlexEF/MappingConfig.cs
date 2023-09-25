using AutoMapper;
using SweatFlexData.Create.DTOs;
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

            CreateMap<Workout, WorkoutDTO>()
                .ForMember(x => x.Creator, opt => opt.MapFrom(src => src.CreatorNavigation))
                .ReverseMap();

            CreateMap<WorkoutUpdateDTO, Workout>().ReverseMap();
            CreateMap<WorkoutCreateDTO, Workout>()
                .ForMember(x => x.Creator, opt => opt.MapFrom(src => src.CreatorId))
                .ReverseMap();

            CreateMap<WorkoutExerciseDTO, Exercise>().ReverseMap();
            CreateMap<WorkoutExerciseUpdateDTO, Exercise>().ReverseMap();
            CreateMap<WorkoutExerciseCreateDTO, Exercise>().ReverseMap();
        }
    }
}
