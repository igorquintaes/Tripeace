using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class GuildInvite
    {
        public int PlayerId { get; set; }
        public int GuildId { get; set; }

        public virtual Guild Guild { get; set; }
        public virtual Player Player { get; set; }
    }
}
