using Tripeace.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.EF.Repository.Server
{
    public partial class ServerRepository : RepositoryBase, IServerRepository
    {
        public ServerRepository(ServerContext context)
            : base(context)
        {

        }
    }
}
