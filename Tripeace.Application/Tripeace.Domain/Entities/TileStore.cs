using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class TileStore
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public int HouseId { get; set; }

        public virtual House House { get; set; }
    }
}
