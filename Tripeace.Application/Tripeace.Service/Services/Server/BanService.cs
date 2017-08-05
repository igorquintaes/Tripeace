using Microsoft.AspNetCore.Http;
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
using Tripeace.Service.Services.Server.Contracts;

namespace Tripeace.Service.Services.Server
{
    public class BanService : ServiceBase, IBanService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBanRepository _banRepository;
        private readonly IBanHistoryRepository _banHistoryRepository;
        private readonly UserManager<AccountIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BanService(
            IAccountRepository accountRepository,
            IBanRepository banRepository,
            IBanHistoryRepository banHistoryRepository,
            UserManager<AccountIdentity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _accountRepository = accountRepository;
            _banRepository = banRepository;
            _banHistoryRepository = banHistoryRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task BanAccount(BanDTO dto)
        {
            if (dto.Date <= DateTime.Now)
            {
                throw new InvalidDateTimeException();
            }

            var account = await _accountRepository.GetById(dto.Id);
            if (account == null)
            {
                // Invalid Id request
                throw new InvalidIdException();
            }

            var bannedBy = await _accountRepository.GetByName(dto.AdminAccount);
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
                    await SendAccountBanToHistory(account);
                }
            }

            var accountBan = new AccountBan()
            {
                Account = account,
                BannedAt = DateTime.Now,
                BannedBy = bannedByPlayer,
                ExpiresAt = dto.Date,
                Reason = dto.Reason?.Trim() ?? String.Empty
            };
            
            await _banRepository.Insert(accountBan);
        }

        public async Task UnbanAccount(UnbanDTO dto)
        {
            var account = await _accountRepository.GetById(dto.Id);
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

            // Outdated ban. Needs to move to history - It is still not banned
            if (account.AccountBan.ExpiresAt <= DateTime.Now)
            {
                await SendAccountBanToHistory(account);
                throw new NoAccountBanException();
            }
            
            await SendAccountBanToHistory(account, DateTime.Now);

            // TODO: send an e-mail explaining the unban reason or just saying the account was unbanned.
        }

        public async Task<bool> IsBanned(int id)
        {
            var account = await _accountRepository.GetById(id);
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
                await SendAccountBanToHistory(account);
                return false;
            }

            return true;
        }

        private async Task SendAccountBanToHistory(Account account, DateTime? expiresTime = null)
        {
            var accountBanHistory = new AccountBanHistory
            {
                Account = account,
                BannedAt = account.AccountBan.BannedAt,
                BannedBy = account.AccountBan.BannedBy,
                ExpiredAt = expiresTime ?? account.AccountBan.ExpiresAt,
                Reason = account.AccountBan.Reason,
            };

            await _banHistoryRepository.Insert(accountBanHistory);
            await _banRepository.Delete(account.AccountBan);
        }
    }
}
