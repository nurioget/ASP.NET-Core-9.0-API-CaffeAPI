﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Dtos.AuthDtos
{
    public class TokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
