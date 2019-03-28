using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringShouldBeEqualToRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private ValidatableObject<string> _validatableObjectToBeEqualTo;
        private readonly string _fieldToBeEqualTo;

        public bool Check(string value)
        {
            return value.Equals(_validatableObjectToBeEqualTo.Value);
        }

        public StringShouldBeEqualToRule(ValidatableObject<string> validatableObjectToBeEqualTo, string fieldToBeEqualTo)
        {
            _validatableObjectToBeEqualTo = validatableObjectToBeEqualTo;
            _fieldToBeEqualTo = fieldToBeEqualTo;

            ValidationMessage = $"Current field must be equal to {_fieldToBeEqualTo}.";
        }
    }
}
