using Tripeace.Domain.Enums;
using Tripeace.Service.DTO.Guild;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Character
{
    public class EditCharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Vocation Vocation { get; set; }
        public Sex Sex { get; set; }
        public bool IsVisible { get; set; }
    }
}
