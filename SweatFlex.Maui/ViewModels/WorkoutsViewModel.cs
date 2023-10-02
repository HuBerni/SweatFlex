using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SweatFlex.Maui.Models;

namespace SweatFlex.Maui.ViewModels
{
    public partial class WorkoutsViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<Workout> _myWorkouts;

        [ObservableProperty]
        private List<Workout> _preBuiltWorkouts;

        public WorkoutsViewModel()
        {
            MyWorkouts = new();
            PreBuiltWorkouts = new();
            MyWorkouts.Add(new Workout() { Id = 1, Name = "Workout 1", Creator = new User() { FirstName = "John", LastName = "Doe" } });   
            MyWorkouts.Add(new Workout() { Id = 1, Name = "Workout 2", Creator = new User() { FirstName = "Jane", LastName = "Doe" } });
            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 1", Creator = new User() { FirstName = "John", LastName = "Doe" } });
            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 2", Creator = new User() { FirstName = "Jane", LastName = "Doe" } });            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 1", Creator = new User() { FirstName = "John", LastName = "Doe" } });
            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 2", Creator = new User() { FirstName = "Jane", LastName = "Doe" } });            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 1", Creator = new User() { FirstName = "John", LastName = "Doe" } });
            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 2", Creator = new User() { FirstName = "Jane", LastName = "Doe" } });            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 1", Creator = new User() { FirstName = "John", LastName = "Doe" } });
            PreBuiltWorkouts.Add(new Workout() { Id = 1, Name = "Workout 2", Creator = new User() { FirstName = "Jane", LastName = "Doe" } });
        }
    }
}
