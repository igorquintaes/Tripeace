using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class BanDTO
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string AdminAccount { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
