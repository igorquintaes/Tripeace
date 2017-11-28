using System.Threading.Tasks;
using Tripeace.Domain.Entities;

namespace Tripeace.Service.Services.Server.Contracts
{
    public interface IAuthorizationService
    {
        Task AssureAdminAuthorization(Account accountTarget, Account accountWhoRequested);
    }
}
