using Tripeace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Service.DTO.Account
{
    public class IndexPlayerDTO
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Vocation Vocation { get; set; }
        public Sex Sex { get; set; }
    }
}
