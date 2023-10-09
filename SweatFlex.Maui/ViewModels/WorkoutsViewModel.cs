using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using SweatFlex.Maui.Models;
using SweatFlex.Maui.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SweatFlex.Maui.ViewModels
{
    public partial class WorkoutsViewModel : ObservableObject
    {
        
        public ObservableCollection<Workout> MyWorkouts { get; set; }

        public ObservableCollection<Workout> PreBuiltWorkouts { get; set; }

        [ObservableProperty]
        private Workout? _selectedWorkout;

        public WorkoutsViewModel()
        {
            MyWorkouts = PopulateWorkoutList();
            PreBuiltWorkouts = PopulateWorkoutList();
        }

        private ObservableCollection<Workout> PopulateWorkoutList()
        {
            var workouts = new ObservableCollection<Workout>()
            {
                new Workout()
                {
                    Id = 1,
                    Name = "Workout 1",
                    Creator = new User()
                    {
                        Id = "1",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = ""
                    },
                    WorkoutExercises = new List<WorkoutExercise>()
                    {
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 1
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 2
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 3
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 2,
                                Name = "Squat",
                                Description = "Put a bar on your back and squat up and down"
                            },
                            WorkoutIndex = 4
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 2,
                                Name = "Squat",
                                Description = "Put a bar on your back and squat up and down"
                            },
                            WorkoutIndex = 5
                        }
                    }
                },
                new Workout()
                {
                    Id = 2,
                    Name = "Workout 2",
                    Creator = new User()
                    {
                        Id = "1",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = ""
                    },
                    WorkoutExercises = new List<WorkoutExercise>()
                    {
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 1
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 2
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 3
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 2,
                                Name = "Squat",
                                Description = "Put a bar on your back and squat up and down"
                            },
                            WorkoutIndex = 4
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 2,
                                Name = "Squat",
                                Description = "Put a bar on your back and squat up and down"
                            },
                            WorkoutIndex = 5
                        }
                    }
                },
                new Workout()
                {
                    Id = 2,
                    Name = "Workout 3",
                    Creator = new User()
                    {
                        Id = "1",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = ""
                    },
                    WorkoutExercises = new List<WorkoutExercise>()
                    {
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 1
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 2
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 1,
                                Name = "Bench Press",
                                Description = "Lay on a bench and press the bar up and down"
                            },
                            WorkoutIndex = 3
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 2,
                                Name = "Squat",
                                Description = "Put a bar on your back and squat up and down"
                            },
                            WorkoutIndex = 4
                        },
                        new WorkoutExercise()
                        {
                            Exercise = new Exercise()
                            {
                                Id = 2,
                                Name = "Squat",
                                Description = "Put a bar on your back and squat up and down"
                            },
                            WorkoutIndex = 5
                        }
                    }
                }
            };

            return workouts;
        }

        [RelayCommand]
        public async Task ShowPopup()
        {
            var result = await Application.Current.MainPage.ShowPopupAsync(new AddWorkoutPopup());

            if(result is string workoutName)
            {
                if (string.IsNullOrWhiteSpace(workoutName))
                    return;

                var workout = new Workout()
                {
                    Name = workoutName
                };

                MyWorkouts.Add(workout);
            }
        }

        [RelayCommand]
        public async Task WorkoutSelected()
        {
            var result = await Application.Current.MainPage.ShowPopupAsync(new ConfirmationPopup("Are you sure you want to start this workout?"));

            if(result is bool confirmed)
            {
                if (confirmed)
                {
                    //TODO, add navigation to workout page
                }

                SelectedWorkout = null;
            }
        }
    }
}
