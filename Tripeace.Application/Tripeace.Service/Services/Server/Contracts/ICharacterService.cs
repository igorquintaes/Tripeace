﻿using Tripeace.Domain.Enums;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.DTO.Character;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripeace.Domain.Contracts;
using Tripeace.Domain.Entities;

namespace Tripeace.Service.Services.Server.Contracts
{
    public interface ICharacterService : IService<Player>
    {
        Task CreateNewCharacter(CreateCharacterDTO data, string accountName);
        Task<EditCharacterDTO> GetCharacterEdit(int id, string accountName);
        Task UpdateCharacter(EditCharacterDTO data, string accountName);
        IEnumerable<Vocation> GetVocationsOnCreate();
    }
}
