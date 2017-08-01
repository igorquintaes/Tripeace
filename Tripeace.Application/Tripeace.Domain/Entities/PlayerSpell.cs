using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class PlayerSpell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
