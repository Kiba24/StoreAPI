﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Dtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
