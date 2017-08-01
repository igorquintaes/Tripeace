using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class IndexDTO
    {
        public bool IsNewAccount { get; set; }
        public string Email { get; set; }
        public string AccountName { get; set; }

        public IEnumerable<IndexPlayerDTO> Characters { get; set; }
    }
}
