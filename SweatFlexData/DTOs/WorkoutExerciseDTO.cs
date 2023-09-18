using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class WorkoutExerciseDTO
    {
        public int Id { get; set; }

        public ExerciseDTO Exercise { get; set; }

        public WorkoutDTO Workout { get; set; }
    }
}
