using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class GuildWar
    {
        public GuildWar()
        {
            GuildwarKills = new HashSet<GuildWarKill>();
        }

        public int Id { get; set; }
        public long _ended { get; set; }
        public int Guild1 { get; set; }
        public int Guild2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public long _started { get; set; }
        public int Status { get; set; }

        public virtual ICollection<GuildWarKill> GuildwarKills { get; set; }

        public DateTime Ended
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_ended);
            }

            set
            {
                _ended = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
        public DateTime Started
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_started);
            }

            set
            {
                _started = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
    }
}
