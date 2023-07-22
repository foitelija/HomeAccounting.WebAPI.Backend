using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using HomeAccounting.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Persistence.Repositories
{
    public class UserRepository : GenericRepository<FamilyMember>, IUserRepository
    {
        private readonly AccountingDbContext _context;

        public UserRepository(AccountingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FamilyMember> FindUserByLoginAsync(string login)
        {
            return await _context.FamilyMembers.FirstOrDefaultAsync(x => x.Login.ToLower().Equals(login.ToLower()));
        }

        public async Task<bool> UserExists(string login)
        {
            if(await _context.FamilyMembers.AnyAsync(user=>user.Login.ToLower().Equals(login.ToLower())))
            {
                return true;
            }
            return false;
        }
    }
}
