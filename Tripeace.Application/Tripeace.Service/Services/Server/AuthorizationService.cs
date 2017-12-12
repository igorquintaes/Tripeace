using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Domain.Entities;
using Tripeace.Domain.Enums;
using Tripeace.Service.Exceptions;
using Tripeace.Service.Services.Server.Contracts;

namespace Tripeace.Service.Services.Server
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<AccountIdentity> _userManager;

        public AuthorizationService(UserManager<AccountIdentity> userManager)
        {
            _userManager = userManager;
        }

        public async Task AssureAdminAuthorization(Account accountTarget, Account accountWhoRequested)
        {
            var accountWhoRequestedRole = (await _userManager.GetRolesAsync(accountWhoRequested.AccountIdentity)).Single();
            var accountTargetRole = (await _userManager.GetRolesAsync(accountTarget.AccountIdentity)).Single();
            Enum.TryParse(accountWhoRequestedRole, out AccountType requesterEnum);
            Enum.TryParse(accountTargetRole, out AccountType targetEnum);

            if (requesterEnum < AccountType.GameMaster ||
                requesterEnum <= targetEnum)
                throw new NoAuthorizationException();
        }
    }
}
