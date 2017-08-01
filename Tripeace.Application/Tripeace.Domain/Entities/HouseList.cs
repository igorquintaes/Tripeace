using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class HouseList
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public string List { get; set; }
        public int Listid { get; set; }

        public virtual House House { get; set; }
    }
}
