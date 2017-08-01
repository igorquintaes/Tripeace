using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tripeace.Domain.Enums
{
    public enum Vocation
    {
        None = 0,
        Sorcerer = 1,
        Druid = 2,
        Paladin = 3,
        Knight = 4,
        [Display(Name = "MasterSorcerer")]
        MasterSorcerer = 5,
        ElderDruid = 6,
        RoyalPaladin = 7,
        EliteKnight = 8
    }
}
