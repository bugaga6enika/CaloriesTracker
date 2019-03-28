using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringShouldNotBeEqualToRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private ValidatableObject<string> _validatableObjectToNotBeEqualTo;
        private readonly string _fieldToNotBeEqualTo;

        public bool Check(string value)
        {
            return !value.Equals(_validatableObjectToNotBeEqualTo.Value);
        }

        public StringShouldNotBeEqualToRule(ValidatableObject<string> validatableObjectToNotBeEqualTo, string fieldToNotBeEqualTo)
        {
            _validatableObjectToNotBeEqualTo = validatableObjectToNotBeEqualTo;
            _fieldToNotBeEqualTo = fieldToNotBeEqualTo;

            ValidationMessage = $"Current field must not be equal to {_fieldToNotBeEqualTo}.";
        }
    }
}
