using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Enums;

namespace Tripeace.IoC
{
    public static class DatabaseManager
    {
        private static readonly string[] Roles = new string[]
        {
            AccountType.Player.ToString(),
            AccountType.Tutor.ToString(),
            AccountType.SeniorTutor.ToString(),
            AccountType.GameMaster.ToString(),
            AccountType.God.ToString(),
        };

        public static async Task SeedRoles(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;

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
