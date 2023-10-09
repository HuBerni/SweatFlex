using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlex.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ExercisesViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<Exercise> _exercises;

        public ExercisesViewModel()
        {
            Exercises = new();
            Exercises.Add(new Exercise() { Id = 1, Name = "Exercise 1", Creator = new User() { FirstName = "John", LastName = "Doe" } });
            Exercises.Add(new Exercise() { Id = 2, Name = "Exercise 2", Creator = new User() { FirstName = "Jane", LastName = "Doe" } });
        }
    }
}
