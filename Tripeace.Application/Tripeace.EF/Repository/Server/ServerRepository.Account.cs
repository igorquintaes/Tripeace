﻿using Microsoft.EntityFrameworkCore;
using Tripeace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.EF.Repository.Server
{
    public partial class ServerRepository
    { 
        public async Task<Account> GetAccount(int id)
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
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetAccountByName(string accountName)
        {
            return await Context.Accounts
                .Where(x => x.Name == accountName)
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

        public async Task<Account> GetAccountByEmail(string email)
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
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public IQueryable<Account> GetAccounts()
        {
            return Context.Accounts
                    .Include(x => x.Players)
                    .Include(x => x.AccountIdentity)
                    .Include(x => x.AccountBan)
                .AsQueryable();
        }
    }
}
