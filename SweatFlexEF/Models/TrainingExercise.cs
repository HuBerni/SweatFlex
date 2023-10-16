﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace SweatFlexEF.Models;

public partial class TrainingExercise
{
    public int Id { get; set; }

    public decimal? Weight { get; set; }

    public int? Reputations { get; set; }

    public int? TimeInSec { get; set; }

    public string UserId { get; set; } = null!;

    public int ExerciseId { get; set; }

    public int WorkoutExerciseId { get; set; }

    public DateTime? ExerciseExecuted { get; set; }

    public int SessionId { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}