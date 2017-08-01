using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class LoginDTO
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
