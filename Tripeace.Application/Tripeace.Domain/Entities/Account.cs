using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tripeace.Domain.Enums;
using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class Account
    {
        public Account()
        {
            AccountBanHistory = new HashSet<AccountBanHistory>();
            AccountViplist = new HashSet<AccountViplist>();
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public int _creation { get; set; }
        public string Email { get; set; }
        public int _lastday { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Premdays { get; set; }
        public string Secret { get; set; }
        public AccountType Type { get; set; }

        public virtual ICollection<AccountBanHistory> AccountBanHistory { get; set; }
        public virtual AccountBan AccountBans { get; set; }
        public virtual AccountIdentity AccountIdentity { get; set; }
        public virtual ICollection<AccountViplist> AccountViplist { get; set; }
        public virtual ICollection<Player> Players { get; set; }

        public DateTime Creation
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_creation);
            }

            set
            {
                _creation = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
        public DateTime LastDay
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_lastday);
            }

            set
            {
                _lastday = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
    }
}
