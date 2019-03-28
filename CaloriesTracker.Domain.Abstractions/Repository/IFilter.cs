namespace CaloriesTracker.Domain.Abstractions.Repository
{
    public interface IFilter
    {
        int? Take { get; }
        int? Skip { get; }
        string SortProperty { get; }
        string SortDirection { get; }
        string FilterExpression { get; }
        object[] FilterValues { get; }
    }
}
