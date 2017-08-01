using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class IpBan
    {
        public int Ip { get; set; }
        public long _bannedAt { get; set; }
        public int BannedBy { get; set; }
        public long _expiresAt { get; set; }
        public string Reason { get; set; }

        public virtual Player BannedByNavigation { get; set; }

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
