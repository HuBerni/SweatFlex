﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace SweatFlexEF.Models;

public partial class Workout
{
    public int Id { get; set; }

    public string Creator { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual User CreatorNavigation { get; set; } = null!;

    public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}