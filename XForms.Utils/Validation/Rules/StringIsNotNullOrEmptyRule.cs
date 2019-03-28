using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringIsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public StringIsNotNullOrEmptyRule()
        {
            ValidationMessage = "Current field is required.";
        }
    }
}
