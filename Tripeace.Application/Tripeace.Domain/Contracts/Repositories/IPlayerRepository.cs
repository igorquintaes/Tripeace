using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Entities;

namespace Tripeace.Domain.Contracts.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetById(int id);
        Task<Player> GetByName(string name);
    }
}
