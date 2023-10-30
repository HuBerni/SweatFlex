using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs.Create
{
    public class WorkoutExerciseCreateDTO : IWorkoutExerciseDTO
    {
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public int Index { get; set; }
    }
}
