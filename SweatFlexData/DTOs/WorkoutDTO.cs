﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }

        public UserDTO Creator { get; set; }

        public string Name { get; set; }
    }
}
