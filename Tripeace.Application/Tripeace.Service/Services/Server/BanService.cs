using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;
using Tripeace.Domain.Enums;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.Exceptions;
using Tripeace.Service.Services.Server.Contracts;

namespace Tripeace.Service.Services.Server
{
    public class BanService : ServiceBase<AccountBan>, IBanService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBanRepository _banRepository;
        private readonly IBanHistoryRepository _banHistoryRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AccountIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BanService(
            IAccountRepository accountRepository,
            IBanRepository banRepository,
            IBanHistoryRepository banHistoryRepository,
            IAuthorizationService authorizationService,
            UserManager<AccountIdentity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _accountRepository = accountRepository;
            _banRepository = banRepository;
            _banHistoryRepository = banHistoryRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task BanAccount(BanDTO dto)
        {
            var account = await _accountRepository.GetById(dto.Id);

            if (dto.ExpiresAt <= DateTime.Now)
                throw new InvalidDateTimeException();

            var isBanned = await IsBanned(account);
            if (isBanned)
                throw new AccountAlreadyBannedException();

            var bannedBy = await _accountRepository.GetByName(dto.AdminAccount);
            if (bannedBy == null)
                throw new InvalidAdminAccountException();
            
            await _authorizationService.AssureAdminAuthorization(account, bannedBy);

            // Set a character to set a ban
            // TODO: PASS CHARACTER NAME AS PARAM to remove this if
            var bannedByPlayer =
                bannedBy.Players.FirstOrDefault(x => x.GroupId == PlayerGroup.God) ??
                bannedBy.Players.FirstOrDefault(x => x.GroupId == PlayerGroup.GameMaster) ??
                bannedBy.Players.FirstOrDefault(x => x.GroupId == PlayerGroup.Player) ??
                throw new RequiredAdminCharacterException();

            var accountBan = new AccountBan()
            {
                Account = account,
                BannedAt = DateTime.Now,
                BannedBy = bannedByPlayer,
                ExpiresAt = dto.ExpiresAt,
                Reason = dto.Reason?.Trim() ?? String.Empty
            };
            
            await _banRepository.Insert(accountBan);
        }

        public async Task UnbanAccount(UnbanDTO dto)
        {
            var whoRequested = await _accountRepository.GetByName(dto.AccountWhoRequested);
            if (whoRequested == null)
                throw new InvalidIdException();

            var account = await _accountRepository.GetById(dto.Id);
            var isBanned = await IsBanned(account);
            if (!isBanned)
                throw new AccountIsNotBannedException();

            await _authorizationService.AssureAdminAuthorization(account, whoRequested);            
            await SendAccountBanToHistory(account, DateTime.Now);

            // TODO: send an e-mail explaining the unban reason or just saying the account was unbanned.
        }

        public async Task<bool> IsBanned(Account account)
        {
            if (account == null)
                throw new InvalidIdException();

            if (account.AccountBan == null)
                return false;

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
