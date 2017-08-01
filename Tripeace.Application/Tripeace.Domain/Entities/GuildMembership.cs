using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class GuildMembership
    {
        public int PlayerId { get; set; }
        public int GuildId { get; set; }
        public string Nick { get; set; }
        public int RankId { get; set; }

        public virtual Guild Guild { get; set; }
        public virtual Player Player { get; set; }
        public virtual GuildRank Rank { get; set; }
    }
}
