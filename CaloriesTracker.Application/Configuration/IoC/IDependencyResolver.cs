namespace CaloriesTracker.Application.Configuration.IoC
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
    }
}
