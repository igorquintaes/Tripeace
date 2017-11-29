using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tripeace.Application.MVC.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CharacterQuantityAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value. 
        readonly char _character;
        readonly int _quantity;

        public char Character => _character;
        public int Quantity => _quantity;

        public CharacterQuantityAttribute(char character, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("quantity field can't be less or equals than 0");

            _character = character;
            _quantity = quantity;
        }

        public override bool IsValid(object value)
        {
            return value != null && 
                   value.ToString().Count(x => x == Character) <= Quantity;
        }
    }
}
