using System;

namespace Tripeace.Domain.Enums
{
    [Flags]
    public enum AccountType
    {
        Player      = 0,
        Tutor       = 1 << 0,
        SeniorTutor = 1 << 1,
        GameMaster  = 1 << 2,
        God         = 1 << 3 
    }
}
