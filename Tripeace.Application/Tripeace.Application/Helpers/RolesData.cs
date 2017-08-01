using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tripeace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.Helpers
{
    public static class RolesData
    {
        private static readonly string[] Roles = new string[]
        {
            AccountType.Player.ToString(),
            AccountType.Tutor.ToString(),
            AccountType.SeniorTutor.ToString(),
            AccountType.GameMaster.ToString(),
            AccountType.God.ToString(),
        };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }
    }
}
