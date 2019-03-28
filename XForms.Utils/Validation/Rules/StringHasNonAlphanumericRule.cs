using System.Linq;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringHasNonAlphanumericRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Any(x => !char.IsLetterOrDigit(x));
        }

        public StringHasNonAlphanumericRule()
        {
            ValidationMessage = $"Current field must contain at least one non alphanumeric symbol.";
        }
    }
}
