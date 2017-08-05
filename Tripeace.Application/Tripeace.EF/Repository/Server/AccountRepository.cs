using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;

namespace Tripeace.EF.Repository.Server
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ServerContext context)
            : base(context)
        {

        }

        public async Task<Account> GetById(int id)
        {
            return await Context.Accounts
                .Where(x => x.Id == id)
                    .Include(x => x.Players)
                    .Include(x => x.AccountIdentity)
                    .Include(x => x.AccountBan)
                        .ThenInclude(x => x.Account)
                    .Include(x => x.AccountBan)
                        .ThenInclude(x => x.BannedBy)
                    .Include(x => x.AccountBanHistory)
                        .ThenInclude(x => x.Account)
                    .Include(x => x.AccountBanHistory)
                        .ThenInclude(x => x.BannedBy)
                .SingleOrDefaultAsync();
        }

        public async Task<Account> GetByName(string name)
        {
            return await Context.Accounts
                .Where(x => x.Name == name)
                    .Include(x => x.Players)
                    .Include(x => x.AccountIdentity)
                    .Include(x => x.AccountBan)
                        .ThenInclude(x => x.Account)
                    .Include(x => x.AccountBan)
                        .ThenInclude(x => x.BannedBy)
                    .Include(x => x.AccountBanHistory)
                        .ThenInclude(x => x.Account)
                    .Include(x => x.AccountBanHistory)
                        .ThenInclude(x => x.BannedBy)
                .SingleOrDefaultAsync();
        }

        public async Task<Account> GetByEmail(string email)
        {
            return await Context.Accounts
                    .Where(x => x.Email == email)
                    .Include(x => x.Players)
                    .Include(x => x.AccountIdentity)
                    .Include(x => x.AccountBan)
                        .ThenInclude(x => x.Account)
                    .Include(x => x.AccountBan)
                        .ThenInclude(x => x.BannedBy)
                    .Include(x => x.AccountBanHistory)
                        .ThenInclude(x => x.Account)
                    .Include(x => x.AccountBanHistory)
                        .ThenInclude(x => x.BannedBy)
                .SingleOrDefaultAsync();
        }
    }
}
