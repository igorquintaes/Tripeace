using System;
using System.Collections.Generic;
using System.Text;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;

namespace Tripeace.EF.Repository.Server
{
    public class BanHistoryRepository : RepositoryBase<AccountBanHistory>, IBanHistoryRepository
    {
        public BanHistoryRepository(ServerContext context)
                : base(context)
        {

        }
    }
}
