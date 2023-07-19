﻿using HomeAccounting.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain
{
    public class FamilyMember : BaseDomainEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
