using AutoMapper;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexEF
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ExerciseDTO, Exercise> ().ReverseMap();
            CreateMap<ExerciseUpdateDTO, Exercise>().ReverseMap();
            CreateMap<ExerciseCreateDTO, Exercise>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, User>().ReverseMap();
        }
    }
}
