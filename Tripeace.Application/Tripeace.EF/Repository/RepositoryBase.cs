using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;

namespace Tripeace.EF.Repository
{
    public abstract class RepositoryBase : IRepository
    {
        protected ServerContext Context { get; set; }

        public RepositoryBase(ServerContext _context)
        {
            this.Context = _context;
        }

        public async Task CommitChanges()
        {
            if (this.Context == null)
            {
                return;
            }

            await this.Context.SaveChangesAsync();
        }
    }
}
