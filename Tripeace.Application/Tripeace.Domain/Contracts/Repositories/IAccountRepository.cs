using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Entities;

namespace Tripeace.Domain.Contracts.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetById(int id);
        Task<Account> GetByName(string name);
        Task<Account> GetByEmail(string email);
    }
}
