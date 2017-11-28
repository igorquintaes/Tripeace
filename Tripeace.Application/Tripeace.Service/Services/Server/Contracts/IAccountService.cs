using Tripeace.Service.DTO.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Service.DTO;
using Tripeace.Domain.Contracts;
using Tripeace.Domain.Entities;

namespace Tripeace.Service.Services.Server.Contracts
{
    public interface IAccountService : IService<Account>
    {
        Task<IEnumerable<string>> TryLogin(LoginDTO data);
        Task LogOff();
        Task<IEnumerable<string>> TryRegisterAccount(RegisterDTO data);
        Task<IndexDTO> GetPlayerInfoIndex(string accountName);
        Task<AccountListDTO> GetAccountList(int? pageNumber, string searchKey);
        Task<int> GetCharactersQuantity(string accountName);
        Task LockAccount(int id, string accountWhoRequested);
        Task UnlockAccount(int id, string accountWhoRequested);
        Task DeleteAccount(int id, string accountWhoRequested);
        Task<AccountToAdminEditDTO> GetAccountToAdminEdit(int id, string accountWhoRequested);
        Task SetAccountToAdminEdit(AccountToAdminEditDTO dto);
    }
}