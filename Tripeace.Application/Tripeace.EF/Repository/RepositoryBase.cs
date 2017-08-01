using Microsoft.EntityFrameworkCore;
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

        public void CommitChanges()
        {
            if (this.Context == null)
            {
                return;
            }

            this.Context.SaveChanges();
        }
    }
}
