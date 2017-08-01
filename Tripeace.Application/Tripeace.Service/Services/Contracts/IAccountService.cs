﻿using Tripeace.Service.DTO.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.Service.Services.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<string>> TryLogin(LoginDTO data);
        Task LogOff();
        Task<IEnumerable<string>> TryRegisterAccount(RegisterDTO data);
        Task<IndexDTO> GetPlayerInfoIndex(string accountName);
        Task<AccountListDTO> GetAccountList(int? pageNumber, string searchKey);
        Task<int> GetCharactersQuantity(string accountName);
    }
}