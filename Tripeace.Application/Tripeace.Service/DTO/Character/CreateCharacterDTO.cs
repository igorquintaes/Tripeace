using Tripeace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Character
{
    public class CreateCharacterDTO
    {
        public string Name { get; set; }
        public Vocation Vocation { get; set; }
        public Sex Sex { get; set; }
    }
}
