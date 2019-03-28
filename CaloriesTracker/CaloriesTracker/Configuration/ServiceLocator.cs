using CaloriesTracker.Application.Configuration.IoC;
using Prism.Ioc;
using System;

namespace CaloriesTracker.Configuration
{
    internal class ServiceLocator : IDependencyResolver
    {
        private static readonly Lazy<ServiceLocator> _lazy = new Lazy<ServiceLocator>(() => new ServiceLocator());

        public static ServiceLocator Current => _lazy.Value;

        private IContainerProvider _container;

        private ServiceLocator()
        {
            _container = ((App)Xamarin.Forms.Application.Current).Container;
        }

        public T Resolve<T>()
        {
            try
            {
                _container = ((App)Xamarin.Forms.Application.Current).Container;
                return _container.Resolve<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
