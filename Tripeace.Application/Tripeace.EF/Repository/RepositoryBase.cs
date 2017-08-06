using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;

namespace Tripeace.EF.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ServerContext Context { get; set; }
        protected DbSet<TEntity> Entity;

        public RepositoryBase(ServerContext _context)
        {
            Context = _context;
            Entity = Context.Set<TEntity>();
        }

        public async Task Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity.Add(entity);
            await CommitChanges();
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await CommitChanges();
        }

        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity.Remove(entity);
            await CommitChanges();
        }

        public IQueryable<TEntity> Query()
        {
            return Entity.AsQueryable();
        }

        private async Task CommitChanges()
        {
            if (Context == null)
            {
                return;
            }

            await Context.SaveChangesAsync();
        }
    }
}
