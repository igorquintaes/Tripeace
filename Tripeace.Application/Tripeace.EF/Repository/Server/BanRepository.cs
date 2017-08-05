using System;
using System.Collections.Generic;
using System.Text;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;

namespace Tripeace.EF.Repository.Server
{
    public class BanRepository : RepositoryBase<AccountBan>, IBanRepository
    {
        public BanRepository(ServerContext context)
            : base(context)
        {

        }
    }
}
