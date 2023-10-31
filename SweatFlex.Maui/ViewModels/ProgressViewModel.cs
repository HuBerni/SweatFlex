using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
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
        private readonly ProgressService _progressService;

        public ProgressViewModel(ProgressService progressService)
        {
            _progressService = progressService;

            
        }


        [RelayCommand]
        public async Task ProgressSelected()
        {
            throw new NotImplementedException();
        }
    }
}
