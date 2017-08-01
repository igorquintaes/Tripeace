﻿using Tripeace.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.ViewModels.Account
{
    public class EditCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Vocation Vocation { get; set; }
        public Sex Sex { get; set; }
        public bool IsVisible { get; set; }
    }
}