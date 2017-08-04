﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;
using Tripeace.Domain.Enums;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.Exceptions;
using Tripeace.Service.Services.Contracts;

namespace Tripeace.Service.Services
{
    public class BanService : IBanService
    {
        private readonly IServerRepository _serverRepository;
        private readonly UserManager<AccountIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BanService(IServerRepository serverRepository,
            UserManager<AccountIdentity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _serverRepository = serverRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task BanAccount(BanDTO dto)
        {
            if (dto.Date <= DateTime.Now)
            {
                throw new InvalidDateTimeException();
            }

            var account = await _serverRepository.GetAccount(dto.Id);
            if (account == null)
            {
                // Invalid Id request
                throw new InvalidIdException();
            }

            var bannedBy = await _serverRepository.GetAccountByName(dto.AdminAccount);
            if (bannedBy == null)
            {
                // Invalid admin account request
                throw new InvalidAdminAccountException();
            }

            var bannedByRole = (await _userManager.GetRolesAsync(bannedBy.AccountIdentity)).Single();
            if (bannedByRole != AccountType.GameMaster.ToString() &&
                bannedByRole != AccountType.God.ToString())
            {
                // not requested by an admin or god
                throw new InvalidAdminAccountException();
            }

            // Set a character to set a ban
            // Character as God, character as admin
            var bannedByPlayer =
                bannedBy.Players.FirstOrDefault(x => x.GroupId == PlayerGroup.God) ??
                bannedBy.Players.FirstOrDefault(x => x.GroupId == PlayerGroup.GameMaster) ??
                bannedBy.Players.FirstOrDefault(x => x.GroupId == PlayerGroup.Player) ??
                throw new RequiredAdminCharacterException();
            
            // Check if there is a ban
            if (account.AccountBan != null)
            {
                // Acount still banned
                if (account.AccountBan.ExpiresAt > DateTime.Now)
                {
                    throw new AccountAlreadyBannedException();
                }

                // Outdated ban - send it to history table
                else
                {
                    SendAccountBanToHistory(account);
                }
            }

            var accountBan = new AccountBan();
            accountBan.Account = account;
            accountBan.BannedAt = DateTime.Now;
            accountBan.BannedBy = bannedByPlayer;
            accountBan.ExpiresAt = dto.Date;
            accountBan.Reason = dto.Reason?.Trim() ?? String.Empty;

            account.AccountBan = accountBan;

            await _serverRepository.CommitChanges();
        }

        public async Task UnbanAccount(UnbanDTO dto)
        {
            var account = await _serverRepository.GetAccount(dto.Id);
            if (account == null)
            {
                // Invalid Id request
                throw new InvalidIdException();
            }

            if (account.AccountBan == null)
            {
                // Impossible to unban a unbanned account lol
                throw new NoAccountBanException();
            }

            // Needs to move to history - It is still not banned
            if (account.AccountBan.ExpiresAt <= DateTime.Now)
            {
                SendAccountBanToHistory(account);
                await _serverRepository.CommitChanges();

                throw new NoAccountBanException();
            }
            
            SendAccountBanToHistory(account, DateTime.Now);
            account.AccountBan = null;

            await _serverRepository.CommitChanges();

            // TODO: send an e-mail explaining the unban reason or just saying the account was unbanned.
        }

        public async Task<bool> IsBanned(int id)
        {
            var account = await _serverRepository.GetAccount(id);
            if (account == null)
            {
                // Invalid Id request
                throw new InvalidIdException();
            }

            if (account.AccountBan == null)
            {
                return false;
            }

            if (account.AccountBan.ExpiresAt <= DateTime.Now)
            {
                SendAccountBanToHistory(account);
                await _serverRepository.CommitChanges();
                return false;
            }

            return true;
        }

        private void SendAccountBanToHistory(Account account, DateTime? expiresTime = null)
        {
            var accountBanHistory = new AccountBanHistory();
            accountBanHistory.Account = account;
            accountBanHistory.BannedAt = account.AccountBan.BannedAt;
            accountBanHistory.BannedBy = account.AccountBan.BannedBy;
            accountBanHistory.ExpiredAt = expiresTime ?? account.AccountBan.ExpiresAt;
            accountBanHistory.Reason = account.AccountBan.Reason;

            account.AccountBanHistory.Add(accountBanHistory);
        }
    }
}
