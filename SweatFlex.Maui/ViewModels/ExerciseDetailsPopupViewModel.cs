using CommunityToolkit.Mvvm.ComponentModel;
using SweatFlex.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ExerciseDetailsPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private Exercise _exercise;

        public ExerciseDetailsPopupViewModel(Exercise exercise)
        {
            Exercise = exercise;
        }

    }
}
