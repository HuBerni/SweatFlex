using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ProgressViewModel : ObservableObject
    {
        private readonly ProgressService _progressService;

        [ObservableProperty]
        private bool _isBusy;

        public ObservableCollection<Progress> Progresses { get; set; }

        public ProgressViewModel(ProgressService progressService)
        {
            _progressService = progressService;
            Progresses = new ObservableCollection<Progress>();
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;
            await SetProgresses();
            IsBusy = false;
        }

        private async Task SetProgresses()
        {
            Progresses.Clear();
            var progresses = await _progressService.GetProgresses(Preferences.Get("UserId", ""));

            progresses?.ToList().ForEach(Progresses.Add);
        }

        [RelayCommand]
        public async Task ProgressSelected()
        {
            throw new NotImplementedException();
        }
    }
}
