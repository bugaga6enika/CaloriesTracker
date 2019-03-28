using System.Linq;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringHasUpperCaseRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Any(char.IsUpper);
        }

        public StringHasUpperCaseRule()
        {
            ValidationMessage = $"Current field must contain at least one upper case character.";
        }
    }
}
