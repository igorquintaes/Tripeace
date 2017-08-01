using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class AccountViplist
    {
        public int AccountId { get; set; }
        public int PlayerId { get; set; }
        public string Description { get; set; }
        public byte Icon { get; set; }
        public bool Notify { get; set; }

        public virtual Account Account { get; set; }
        public virtual Player Player { get; set; }
    }
}
