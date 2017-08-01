using Tripeace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.ViewModels.Account
{
    public class Index
    {
        public bool IsNewAccount { get; set; }
        public string Email { get; set; }
        public string AccountName { get; set; }

        public IEnumerable<IndexPlayer> Characters { get; set; }
    }
}
