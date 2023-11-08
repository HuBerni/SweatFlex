using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Services;
using SweatFlexAPIClient.Services;
using System.Collections.ObjectModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ProgressViewModel : ObservableObject
    {
        private readonly ProgressService _progressService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _isCoach;

        [ObservableProperty]
        private string? _userId;

        public ObservableCollection<User> Users { get; set; } = new();

        public ObservableCollection<Progress> Progresses { get; set; }

        public ProgressViewModel(ProgressService progressService, UserService userService, IMapper mapper)
        {
            _progressService = progressService;
            _userService = userService;
            _mapper = mapper;
            Progresses = new ObservableCollection<Progress>();
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            if (Preferences.Get("RoleId", "") == "2" && Users.Count == 0)
            {
                IsCoach = true;
                var response = await _userService.GetUserByCoachAsync(Preferences.Get("UserId", ""));
                var user = response.Result?.Select(_mapper.Map<User>).ToList();
                user?.ForEach(Users.Add);
            }
            else if (Preferences.Get("RoleId", "") == "2")
            {
                IsCoach = true;
            }
            else
            {
                IsCoach = false;
            }

            await SetProgresses();
            IsBusy = false;
        }

        public async Task SetProgresses()
        {
            IsBusy = true;
            var progresses = new List<Progress>();
            Progresses.Clear();
            if (!string.IsNullOrEmpty(UserId) && IsCoach)
            {
                progresses = await _progressService.GetProgresses(UserId);
            }
            else
            {
                progresses = await _progressService.GetProgresses(Preferences.Get("UserId", ""));
            }

            progresses?.ToList().ForEach(Progresses.Add);
            IsBusy = false;
        }

        [RelayCommand]
        public async Task ProgressSelected()
        {
            throw new NotImplementedException();
        }
    }
}
