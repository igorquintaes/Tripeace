using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class RegisterDTO
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool AgreeReciveNews { get; set; }
    }
}
