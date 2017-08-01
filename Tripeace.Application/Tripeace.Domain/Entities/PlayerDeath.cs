using Tripeace.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace Tripeace.Domain.Entities
{
    public partial class PlayerDeath
    {
        public int Id { get; set; }
        public bool IsPlayer { get; set; }
        public string KilledBy { get; set; }
        public int Level { get; set; }
        public string MostdamageBy { get; set; }
        public bool MostdamageIsPlayer { get; set; }
        public bool MostdamageUnjustified { get; set; }
        public int PlayerId { get; set; }
        public long _time { get; set; }
        public bool Unjustified { get; set; }

        public virtual Player Player { get; set; }

        public DateTime Time
        {
            get
            {
                return DateTimeHelper.FromUnixTime(_time);
            }

            set
            {
                _time = DateTimeHelper.ToUnixTimeLong(value);
            }
        }
    }
}
