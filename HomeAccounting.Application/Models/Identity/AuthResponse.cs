﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Models.Identity
{
    public class AuthResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
