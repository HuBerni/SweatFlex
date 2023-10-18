using CommunityToolkit.Maui.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.Models
{
    public class Progress
    {
        public Workout Workout { get; set; }
        public ObservableCollection<TrainingExercise> TrainingExercises { get; set; }

    }
}
