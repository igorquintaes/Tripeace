using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Domain.Contracts
{
    public interface IRepository
    {
        void CommitChanges();
    }
}
