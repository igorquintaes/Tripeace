using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class MarketOffer
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public bool Anonymous { get; set; }
        public long _created { get; set; }
        public int Itemtype { get; set; }
        public int PlayerId { get; set; }
        public int Price { get; set; }
        public bool Sale { get; set; }

        public virtual Player Player { get; set; }

        public DateTime CreatedAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_created);
            }

            set
            {
                _created = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
    }
}
