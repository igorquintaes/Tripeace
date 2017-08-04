using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.Service.Services.Contracts
{
    public interface IBanService
    {
        Task BanAccount(int id, string reason, string accountNameAdmin);
        Task UnbanAccount(int id, string reason);
        Task<bool> IsBanned(int id);
    }
}
