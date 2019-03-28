using System.Linq;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringHasLowerCaseRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Any(char.IsLower);
        }

        public StringHasLowerCaseRule()
        {
            ValidationMessage = $"Current field must contain at least one lower case character.";
        }
    }
}
