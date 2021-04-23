using System;
using System.ComponentModel.DataAnnotations;

namespace desafio_rdi.webapi.CustomValidators
{
    public class NumberAttribute : ValidationAttribute
    {
        
        public int Positions { get; set; }
        public bool Exact { get; set; }

        public NumberAttribute()
        {
        }

        public NumberAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public NumberAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
         
            var valueString = value.ToString();
            long number;

            if (!long.TryParse(valueString, out number)) return false;

            if (number <= 0) return false;

            var lengthField = valueString.Length;

            if (Exact && lengthField != Positions) return false;

            if(Positions >0) return lengthField <= Positions;

            return true;
        }
    }
}
