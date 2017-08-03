using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.Domain.Contracts
{
    public interface IRepository
    {
        Task CommitChanges();
    }
}
