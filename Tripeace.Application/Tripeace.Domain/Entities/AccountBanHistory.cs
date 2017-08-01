using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class AccountBanHistory
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public long _bannedAt { get; set; }
        public int BannedById { get; set; }
        public long _expiredAt { get; set; }
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
        public DateTime ExpiredAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_expiredAt);
            }

            set
            {
                _expiredAt = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
    }
}
