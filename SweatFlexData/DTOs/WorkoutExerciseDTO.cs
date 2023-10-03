using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class WorkoutExerciseDTO : IWorkoutExerciseDTO
    {
        public int Id { get; set; }
        public ExerciseDTO Exercise { get; set; }
        public WorkoutDTO Workout { get; set; }
        public int WorkoutIndex { get; set; }
    }
}
