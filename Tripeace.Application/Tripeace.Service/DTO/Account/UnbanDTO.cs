using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class UnbanDTO
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string AccountWhoRequested { get; set; }
    }
}
