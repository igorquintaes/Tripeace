using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;
using Tripeace.Domain.Entities;
using Tripeace.Service.DTO.Account;

namespace Tripeace.Service.Services.Server.Contracts
{
    public interface IBanService : IService<AccountBan>
    {
        Task BanAccount(BanDTO dto);
        Task UnbanAccount(UnbanDTO dto);
        Task<bool> IsBanned(Account account);
    }
}
