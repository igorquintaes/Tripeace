using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class GuildRank
    {
        public GuildRank()
        {
            GuildMembership = new HashSet<GuildMembership>();
        }

        public int Id { get; set; }
        public int GuildId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GuildMembership> GuildMembership { get; set; }
        public virtual Guild Guild { get; set; }
    }
}
