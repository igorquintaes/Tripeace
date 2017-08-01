using Microsoft.EntityFrameworkCore;
using Tripeace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.EF.Repository.Server
{
    public partial class ServerRepository
    {
        public void AddCharacter(Player entity)
        {
            Context.Players.Add(entity);
        }
        public async Task<Player> GetCharacterByName(string characterName)
        {
            return await Context.Players.FirstOrDefaultAsync(x => x.Name == characterName);
        }

        public async Task<Player> GetCharacter(int id)
        {
            return await Context.Players.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
