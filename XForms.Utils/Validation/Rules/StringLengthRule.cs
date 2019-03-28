using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringLengthRule : IValidationRule<string>
    {
        private readonly uint _length;

        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Length >= _length;
        }

        public StringLengthRule(uint length)
        {
            _length = length;
            ValidationMessage = $"Length of the current field must be greater or equal to {_length}.";
        }
    }
}
