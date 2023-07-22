using HomeAccounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Interfaces.Persistence
{
    public interface IUserRepository : IGenericRepository<FamilyMember>
    {
        Task<bool> UserExists(string login);
        Task<FamilyMember> FindUserByLoginAsync(string login);
    }
}
