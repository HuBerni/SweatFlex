using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public Workout Workout { get; set; }
        public int Index { get; set; }
    }
}
