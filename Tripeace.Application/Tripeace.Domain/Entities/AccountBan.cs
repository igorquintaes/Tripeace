using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class AccountBan
    {
        public int AccountId { get; set; }
        public long _bannedAt { get; set; }
        public int BannedById { get; set; }
        public long _expiresAt { get; set; }
        public string Reason { get; set; }

        public virtual Account Account { get; set; }
        public virtual Player BannedBy { get; set; }

        public DateTime BannedAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_bannedAt);
            }

            set
            {
                _bannedAt = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
        public DateTime ExpiresAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_expiresAt);
            }

            set
            {
                _expiresAt = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
    }
}
