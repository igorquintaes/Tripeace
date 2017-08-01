using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class GuildWarKill
    {
        public int Id { get; set; }
        public string Killer { get; set; }
        public int Killerguild { get; set; }
        public string Target { get; set; }
        public int Targetguild { get; set; }
        public long _time { get; set; }
        public int Warid { get; set; }

        public virtual GuildWar War { get; set; }

        public DateTime Time
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_time);
            }

            set
            {
                _time = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
    }
}
