using XForms.Utils.Core;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class EmailShouldBeValidRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return RegexCompiler.Current.Email.IsMatch(value);
        }

        public EmailShouldBeValidRule()
        {
            ValidationMessage = $"Current field is not valid.";
        }
    }
}
