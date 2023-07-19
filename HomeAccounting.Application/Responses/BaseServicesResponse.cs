﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Responses
{
    public class BaseServicesResponse
    {
        public int? Id { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
