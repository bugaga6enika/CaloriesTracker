using System;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;

namespace CaloriesTracker.Domain.Abstractions.Validation
{
    public abstract class ValidationRule<T> : IValidationRule<T>
    {
        protected string ObjectName;

        public ValidationResult ApplyTo(T obj, string objName = null)
        {
            ObjectName = objName;

            try
            {
                return CoreCheck(obj);
            }
            catch (Exception e)
            {
                return InvalidResult(new ValidationException(e.Message, ObjectName, e));
            }
        }

        protected abstract ValidationResult CoreCheck(T obj);
    }
}
