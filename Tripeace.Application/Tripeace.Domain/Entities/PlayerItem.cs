using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class PlayerItem
    {
        public int Id { get; set; }
        public byte[] Attributes { get; set; }
        public short Count { get; set; }
        public short Itemtype { get; set; }
        public int Pid { get; set; }
        public int PlayerId { get; set; }
        public int Sid { get; set; }

        public virtual Player Player { get; set; }
    }
}
