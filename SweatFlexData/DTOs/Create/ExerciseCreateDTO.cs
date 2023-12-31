﻿using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Create
{
    public class ExerciseCreateDTO : IExerciseDTO
    {
        public string Creator { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public int Musclegroup { get; set; }

        public int? Equipment { get; set; }

        public string Description { get; set; }

    }
}
