using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ProgressViewModel : ObservableObject
    {
        public ObservableCollection<Progress> Progresses { get; set; }

        public ProgressViewModel()
        {
            Progresses = new()
            {
                new Progress() { 
                    TrainingExercises = new ObservableCollection<TrainingExercise>() 
                    { 
                        new TrainingExercise(), 
                        new TrainingExercise() 
                    }},
                new Progress(),
                new Progress()
            };
        }


        [RelayCommand]
        public async Task ProgressSelected()
        {
            throw new NotImplementedException();
        }
    }
}
