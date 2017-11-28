using Microsoft.AspNetCore.Identity;

namespace Tripeace.Domain.Entities
{
    public class AccountIdentity : IdentityUser
    {
        public int AccountIdentityId { get; set; }

        public virtual Account Account { get; set; }

        public bool News { get; set; }
    }
}
