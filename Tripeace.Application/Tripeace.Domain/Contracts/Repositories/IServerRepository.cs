using Tripeace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.Domain.Contracts.Repositories
{
    public interface IServerRepository : IRepository
    {
        // Account
        Task<Account> GetAccount(int id);
        Task<Account> GetAccountByName(string accountName);
        Task<Account> GetAccountByEmail(string email);
        IQueryable<Account> GetAccounts();

        // Player
        Task<Player> GetCharacterByName(string characterName);
        Task<Player> GetCharacter(int id);
    }
}
