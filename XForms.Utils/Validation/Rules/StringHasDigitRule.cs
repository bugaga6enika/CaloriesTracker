using System.Linq;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringHasDigitRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Any(char.IsDigit);
        }

        public StringHasDigitRule()
        {
            ValidationMessage = $"Current field must contain at least one digit.";
        }
    }
}
