using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class PlayerNamelock
    {
        public int PlayerId { get; set; }
        public long _namelockedAt { get; set; }
        public int NamelockedBy { get; set; }
        public string Reason { get; set; }

        public virtual Player NamelockedByNavigation { get; set; }
        public virtual Player Player { get; set; }

        public DateTime NamelockedAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_namelockedAt);
            }

            set
            {
                _namelockedAt = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
    }
}
