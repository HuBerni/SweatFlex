﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace SweatFlexEF.Models;

public partial class PasswordDepot
{
    public Guid Id { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}