using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.MVC.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CharacterQuantityAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value. 
        readonly char _character;
        readonly int _quantity;

        public char Character
        {
            get { return _character; }
        }

        public int Quantity
        {
            get { return _quantity; }
        }

        public CharacterQuantityAttribute(char character, int quantity)
        {
            _character = character;

            if (quantity < 0)
                throw new Exception("quantity field can't be less than 0");

            _quantity = quantity;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            return ((string)value).Count(x => x == Character) <= Quantity;
        }
    }
}
