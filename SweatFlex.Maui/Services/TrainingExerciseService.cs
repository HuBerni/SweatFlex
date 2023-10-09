using SweatFlex.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.Services
{
    public class TrainingExerciseService
    {
        public List<TrainingExercise>? CreateTrainingExercisesFromWorkoutExercises(List<WorkoutExercise> workoutExercises)
        {
            var trainingExercises = new List<TrainingExercise>();

            foreach (var trainingExercise in workoutExercises)
            {
                //TODO - API Post TrainingExercises
            }


            return trainingExercises;
        }
    }
}
