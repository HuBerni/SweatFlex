﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public int Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserDTO? Coach { get; set; }
    }
}
