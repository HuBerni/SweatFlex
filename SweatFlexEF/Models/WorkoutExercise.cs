﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SweatFlexEF.Models;

public partial class WorkoutExercise
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public int WorkoutId { get; set; }

    public virtual Exercise Exercise { get; set; }

    public virtual Workout Workout { get; set; }
}