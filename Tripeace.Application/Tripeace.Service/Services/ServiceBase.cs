using System;
using System.Collections.Generic;
using System.Text;
using Tripeace.Domain.Contracts;

namespace Tripeace.Service.Services
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : class
    {
    }
}
