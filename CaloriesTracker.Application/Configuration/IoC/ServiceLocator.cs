using Autofac;
using CaloriesTracker.Application.Pipelines;
using CaloriesTracker.Domain.InternalAuth;
using MediatR;
using System;
using System.Reflection;

namespace CaloriesTracker.Application.Configuration.IoC
{
    public class ServiceLocator : IDependencyResolver
    {
        private readonly IContainer _container;

        private static Lazy<ServiceLocator> _lazyContainer = new Lazy<ServiceLocator>(() => new ServiceLocator());

        public static ServiceLocator Current = _lazyContainer.Value;

        private ServiceLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder.RegisterAssemblyTypes(typeof(ServiceLocator).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            // Application layer
            builder.RegisterAssemblyTypes(typeof(ServiceLocator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
#if DEBUG
            builder.RegisterInstance(Infrastructure.Mock.Rest.HttpClientFactory.Create());
#else
            builder.RegisterInstance(Configurator.HttpClient);
#endif
            builder.RegisterInstance(Configurator.NativeHttpClientService);

            // Infastructure layer
            builder.RegisterAssemblyTypes(typeof(Infrastructure.Configuration.Configurator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // Domain layer
            builder.RegisterAssemblyTypes(typeof(AuthToken).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            _container = builder.Build();
        }

        public T Resolve<T>() => _container.Resolve<T>();
    }
}
