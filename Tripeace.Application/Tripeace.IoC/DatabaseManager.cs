using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;
using Tripeace.Domain.Enums;
using Tripeace.EF;

namespace Tripeace.IoC
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly ServerContext _context;

        public DatabaseManager(ServerContext context)
        {
            _context = context;
        }

        private static readonly string[] Roles = new string[]
        {
            AccountType.Player.ToString(),
            AccountType.Tutor.ToString(),
            AccountType.SeniorTutor.ToString(),
            AccountType.GameMaster.ToString(),
            AccountType.God.ToString(),
        };

        public async Task SeedRoles()
        {
            foreach (var role in Roles)
            {
                if (!_context.Roles.Any(x => x.Name == role))
                {
                    await _context.Roles.AddAsync(new IdentityRole(role));
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
