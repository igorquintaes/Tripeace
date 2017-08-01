using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class AccountListDTO
    {
        public AccountListDTO()
        {
            Accounts = new List<AccountListItemDTO>();
        }

        public List<AccountListItemDTO> Accounts { get; set; }
        public int TotalResults { get; set; }
    }
}
