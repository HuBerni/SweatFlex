using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Create
{
    public class WorkoutCreateDTO : IWorkoutDTO
    {
        public string CreatorId { get; set; }

        public string Name { get; set; }
    }
}
