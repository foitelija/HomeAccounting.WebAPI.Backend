﻿using HomeAccounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Interfaces.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}
