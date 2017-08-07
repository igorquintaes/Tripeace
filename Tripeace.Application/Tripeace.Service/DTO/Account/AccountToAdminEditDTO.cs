using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class AccountToAdminEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool ReciveNews { get; set; }

        public string AccountWhoRequested { get; set; }
    }
}
