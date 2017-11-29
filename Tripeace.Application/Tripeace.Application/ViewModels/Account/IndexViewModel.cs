using System.Collections.Generic;

namespace Tripeace.Application.ViewModels.Account
{
    public class IndexViewModel
    {
        public bool IsNewAccount { get; set; }
        public string Email { get; set; }
        public string AccountName { get; set; }

        public IEnumerable<IndexPlayerViewModel> Characters { get; set; }
    }
}
