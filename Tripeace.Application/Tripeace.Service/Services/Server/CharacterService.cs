using Microsoft.Extensions.Configuration;
using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Domain.Entities;
using Tripeace.Domain.Enums;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.DTO.Character;
using Tripeace.Service.Exceptions;
using Tripeace.Service.Services.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripeace.Service.Services.Server
{
    public class CharacterService : ServiceBase, ICharacterService
    {
        private IPlayerRepository _playerRepository;
        private IAccountRepository _accountRepository;
        private IConfiguration _configuration; 

        public CharacterService(
            IPlayerRepository playerRepository,
            IAccountRepository accountRepository,
            IConfiguration configuration)
        {
            _playerRepository = playerRepository;
            _accountRepository = accountRepository;
            _configuration = configuration;            
        }

        public async Task CreateNewCharacter(CreateCharacterDTO data, string accountName)
        {
            var account = await _accountRepository.GetByName(accountName);

            if (await _playerRepository.GetByName(data.Name) != null)
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
                Sex = data.Sex,
                Account = account
            };
            
            await _playerRepository.Insert(character);
        }

        public async Task<EditCharacterDTO> GetCharacterEdit(int id, string accountName)
        {
            var account = await _accountRepository.GetByName(accountName);
            var character = account.Players.SingleOrDefault(x => x.Id == id);

            if (character == null)
            {
                var characterExists = await _playerRepository.GetById(id);
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
            var account = await _accountRepository.GetByName(accountName);
            var character = account.Players.SingleOrDefault(x => x.Id == data.Id);

            if (character == null)
            {
                var characterExists = await _playerRepository.GetById(data.Id);
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

            await _playerRepository.Update(character);
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
