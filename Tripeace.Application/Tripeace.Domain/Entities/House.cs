using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class House
    {
        public House()
        {
            HouseLists = new HashSet<HouseList>();
            TileStore = new HashSet<TileStore>();
        }

        public int Id { get; set; }
        public int Beds { get; set; }
        public int Bid { get; set; }
        public int BidEnd { get; set; }
        public int HighestBidder { get; set; }
        public int LastBid { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public int Paid { get; set; }
        public int Rent { get; set; }
        public int Size { get; set; }
        public int TownId { get; set; }
        public int Warnings { get; set; }

        public virtual ICollection<HouseList> HouseLists { get; set; }
        public virtual ICollection<TileStore> TileStore { get; set; }

        public int PaidInDays
        {
            get
            {
                return DateTimeHelper.FromUnixTimeDaysPeriod(Paid);
            }
        }
    }
}
