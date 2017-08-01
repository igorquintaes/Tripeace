using AutoMapper;
using Tripeace.Application.Helpers.Mappers.Contracts;
using Tripeace.Application.ViewModels.Account;
using Tripeace.Service.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.Helpers.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        public Index IndexMapper(IndexDTO data)
        {
            return new Index()
            {
                AccountName = data.AccountName,
                Email = data.Email,
                IsNewAccount = data.IsNewAccount,
                Characters = data.Characters.Select(x => new IndexPlayer()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Level = x.Level,
                    Name = x.Name,
                    Sex = x.Sex,
                    Vocation = x.Vocation
                })
            };
        }
    }
}
