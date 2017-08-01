using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class AccountListItemDTO
    {
        public AccountListItemDTO()
        {
            Characters = new List<string>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsLocked { get; set; }
        public IEnumerable<string> Characters { get; set; }
    }
}
