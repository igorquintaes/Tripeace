using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;

namespace Tripeace.EF.Repository.Server
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(ServerContext context)
            : base(context)
        {

        }

        public async Task<Player> GetByName(string name)
        {
            return await Context.Players
                .Where(x => x.Name == name)
                    .Include(x => x.Account)
                    .Include(x => x.Guilds)
                        .ThenInclude(x => x.GuildRanks)
                    .Include(x => x.GuildMembership)
                    .Include(x => x.GuildInvites)
                    .Include(x => x.IpBans)
                    .Include(x => x.PlayerDeaths)
                    .Include(x => x.PlayerNamelocksPlayer)
                .FirstOrDefaultAsync();
        }

        public async Task<Player> GetById(int id)
        {
            return await Context.Players
                .Where(x => x.Id == id)
                    .Include(x => x.Account)
                    .Include(x => x.Guilds)
                        .ThenInclude(x => x.GuildRanks)
                    .Include(x => x.GuildMembership)
                    .Include(x => x.GuildInvites)
                    .Include(x => x.IpBans)
                    .Include(x => x.PlayerDeaths)
                    .Include(x => x.PlayerNamelocksPlayer)
                .FirstOrDefaultAsync();
        }
    }
}
