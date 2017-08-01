using Microsoft.Extensions.Configuration;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;
using Tripeace.Domain.Enums;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.DTO.Character;
using Tripeace.Service.Exceptions;
using Tripeace.Service.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.Service.Services
{
    public class CharacterService : ICharacterService
    {
        private IServerRepository _serverRepository;
        private IConfiguration _configuration; 

        public CharacterService(IServerRepository serverRepository,
            IConfiguration configuration)
        {
            _serverRepository = serverRepository;
            _configuration = configuration;
            
        }

        public async Task CreateNewCharacter(CreateCharacterDTO data, string accountName)
        {
            var account = await _serverRepository.GetAccountByName(accountName);

            if (await _serverRepository.GetCharacterByName(data.Name) != null)
            {
                throw new CharacterAlreadyRegisteredException();
            }

            var allowedVocations = GetVocationsOnCreate();
            if (!allowedVocations.Contains(data.Vocation))
            {
                throw new TryingToSaveInvalidDataException();
            }

            var character = new Player()
            {
                Name = data.Name,
                Vocation = data.Vocation,
                Conditions = new byte[8],
                Sex = data.Sex
            };
            
            account.Players.Add(character);
            _serverRepository.CommitChanges();
        }

        public async Task<EditCharacterDTO> GetCharacterEdit(int id, string accountName)
        {
            var account = await _serverRepository.GetAccountByName(accountName);
            var character = account.Players.FirstOrDefault(x => x.Id == id);

            if (character == null)
            {
                var characterExists = await _serverRepository.GetCharacter(id);
                if (characterExists == null)
                {
                    throw new InvalidIdException();
                }
                else
                {
                    throw new TryingToAccessOtherAccountException();
                }
            }

            return new EditCharacterDTO()
            {
                Id = character.Id,
                Description = character.Description,
                IsVisible = character.IsVisible,
                Name = character.Name,
                Sex = character.Sex,
                Vocation = character.Vocation
            };
        }

        public async Task UpdateCharacter(EditCharacterDTO data, string accountName)
        {
            var account = await _serverRepository.GetAccountByName(accountName);
            var character = account.Players.FirstOrDefault(x => x.Id == data.Id);

            if (character == null)
            {
                var characterExists = await _serverRepository.GetCharacter(data.Id);
                if (characterExists == null)
                {
                    throw new InvalidIdException();
                }
                else
                {
                    throw new TryingToAccessOtherAccountException();
                }
            }

            character.Description = data.Description;
            character.IsVisible = data.IsVisible;
            
            _serverRepository.CommitChanges();
        }

        public IEnumerable<Vocation> GetVocationsOnCreate()
        {
            var vocationsOnCreate = _configuration.AsEnumerable()
                .FirstOrDefault(r => r.Key == "VocationsOnCreate")
                .Value;

            switch (vocationsOnCreate)
            {
                case "Rooker":
                    return new Vocation[]
                    {
                        Vocation.None
                    };
                case "BasicVocations":
                    return new Vocation[]
                    {
                        Vocation.Druid,
                        Vocation.Sorcerer,
                        Vocation.Knight,
                        Vocation.Paladin
                    };
                case "PromotedVocations":
                    return new Vocation[]
                    {
                        Vocation.ElderDruid,
                        Vocation.MasterSorcerer,
                        Vocation.EliteKnight,
                        Vocation.RoyalPaladin
                    };
                default:
                    throw new Exception("Missing key 'VocationsOnCreate' on config.");
            }
        }
    }
}
