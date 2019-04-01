using System;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class EnumShouldBeGreaterThenZeroRule<T> : IValidationRule<T> where T : struct, IConvertible
    {
        public string ValidationMessage { get; set; } = "Enum should be greater then 0";

        public bool Check(T value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (int)(object)value > 0;
        }
    }
}
