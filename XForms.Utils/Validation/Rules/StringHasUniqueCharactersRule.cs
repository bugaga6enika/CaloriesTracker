using System.Linq;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class StringHasUniqueCharactersRule : IValidationRule<string>
    {
        private readonly uint _uniqueCharactersCount;

        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Where(x => char.IsLetter(x)).GroupBy(x => x).Select(x => x.First()).Count() >= _uniqueCharactersCount;
        }

        public StringHasUniqueCharactersRule(uint uniqueCharactersCount)
        {
            _uniqueCharactersCount = uniqueCharactersCount;
            ValidationMessage = $"Current field must contain at least {_uniqueCharactersCount} unique characters.";
        }
    }
}
