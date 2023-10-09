using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Update
{
    public class WorkoutUpdateDTO : IWorkoutDTO
    {
        public string Name { get; set; }
    }
}
