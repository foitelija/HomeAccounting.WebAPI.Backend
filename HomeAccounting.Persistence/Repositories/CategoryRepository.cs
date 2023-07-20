using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using HomeAccounting.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AccountingDbContext context) : base(context) { }

    }
}
