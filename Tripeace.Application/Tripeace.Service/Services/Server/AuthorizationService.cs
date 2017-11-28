using Microsoft.AspNetCore.Identity;
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
            userManager = _userManager;
        }

        public async Task AssureAdminAuthorization(Account accountTarget, Account accountWhoRequested)
        {
            var accountWhoRequestedRole = (await _userManager.GetRolesAsync(accountWhoRequested.AccountIdentity)).Single();
            var accountTargetRole = (await _userManager.GetRolesAsync(accountTarget.AccountIdentity)).Single();

            // God has all privileges
            if (accountWhoRequestedRole == AccountType.God.ToString())
            {
                return;
            }

            // Request has no admin Privileges
            if (accountWhoRequestedRole != AccountType.GameMaster.ToString())
            {
                throw new NoAuthorizationException();
            }

            // Deny request to higher roles if not God
            if (accountTargetRole == AccountType.God.ToString() ||
                accountTargetRole == AccountType.GameMaster.ToString())
            {
                throw new NoAuthorizationException();
            }
        }
    }
}
