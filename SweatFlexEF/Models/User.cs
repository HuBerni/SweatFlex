﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SweatFlexEF.Models;

public partial class User
{
    public string Id { get; set; }

    public int Role { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Coach { get; set; }

    public bool IsActive { get; set; }

    public virtual User CoachNavigation { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<User> InverseCoachNavigation { get; set; } = new List<User>();

    public virtual UserRole RoleNavigation { get; set; }

    public virtual ICollection<TrainingExercise> TrainingExercises { get; set; } = new List<TrainingExercise>();

    public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}