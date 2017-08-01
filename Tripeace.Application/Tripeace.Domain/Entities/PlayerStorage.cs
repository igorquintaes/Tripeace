using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class PlayerStorage
    {
        public int PlayerId { get; set; }
        public int Key { get; set; }
        public int Value { get; set; }

        public virtual Player Player { get; set; }
    }
}
