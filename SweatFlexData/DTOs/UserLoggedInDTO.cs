﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using SweatFlexData.Interface.IDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class UserLoggedInDTO : IAuthDTO
    {
        public string Id { get; set; }

        public int Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserDTO? Coach { get; set; }
        public string Token { get; set; }

    }
}
