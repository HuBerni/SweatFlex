﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SweatFlexEF.Models;

public partial class TrainingExercise
{
    public int Id { get; set; }

    public decimal? Weight { get; set; }

    public int? Reputations { get; set; }

    public int? TimeInSec { get; set; }

    public int UserId { get; set; }

    public DateTime ExerciseExecuted { get; set; }

    public int ExerciseId { get; set; }

    public virtual Exercise Exercise { get; set; }

    public virtual User User { get; set; }
}