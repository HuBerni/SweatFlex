using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Creator { get; set; }
        public List<WorkoutExercise>? WorkoutExercises { get; set; }
    }
}
