namespace CaloriesTracker.Domain.Abstractions.Validation
{
    public interface IValidationRule<T>
    {
        ValidationResult ApplyTo(T obj, string objName = null);
    }
}