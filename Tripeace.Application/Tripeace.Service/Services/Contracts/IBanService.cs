using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Service.DTO.Account;

namespace Tripeace.Service.Services.Contracts
{
    public interface IBanService
    {
        Task BanAccount(BanDTO dto);
        Task UnbanAccount(UnbanDTO dto);
        Task<bool> IsBanned(int id);
    }
}
