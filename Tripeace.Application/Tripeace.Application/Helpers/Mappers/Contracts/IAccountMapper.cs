using Tripeace.Application.ViewModels.Account;
using Tripeace.Service.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.Helpers.Mappers.Contracts
{
    public interface IAccountMapper
    {
        Index IndexMapper(IndexDTO data);
    }
}
