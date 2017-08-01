using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class MarketHistory
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public long _expiresAt { get; set; }
        public long _insertedAt { get; set; }
        public int Itemtype { get; set; }
        public int PlayerId { get; set; }
        public int Price { get; set; }
        public bool Sale { get; set; }
        public bool State { get; set; }

        public virtual Player Player { get; set; }

        public DateTime InsertedAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_insertedAt);
            }

            set
            {
                _insertedAt = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
        public DateTime ExpiresAt
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_expiresAt);
            }

            set
            {
                _expiresAt = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
    }
}
