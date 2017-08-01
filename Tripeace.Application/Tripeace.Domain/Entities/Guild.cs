using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class Guild
    {
        public Guild()
        {
            GuildInvites = new HashSet<GuildInvite>();
            GuildMembership = new HashSet<GuildMembership>();
            GuildRanks = new HashSet<GuildRank>();
        }

        public int Id { get; set; }
        public int _creationdata { get; set; }
        public string Motd { get; set; }
        public string Name { get; set; }
        public int Ownerid { get; set; }

        public virtual ICollection<GuildInvite> GuildInvites { get; set; }
        public virtual ICollection<GuildMembership> GuildMembership { get; set; }
        public virtual ICollection<GuildRank> GuildRanks { get; set; }
        public virtual Player Owner { get; set; }

        public DateTime Creationdata
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_creationdata);
            }

            set
            {
                _creationdata = DateTimeHelper.ToUnixTimeInt(value);
            }
        }
    }
}
