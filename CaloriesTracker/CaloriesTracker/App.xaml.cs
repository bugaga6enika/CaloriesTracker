using CaloriesTracker.ViewModels;
using CaloriesTracker.Views;
using MediatR;
using Prism;
using Prism.Ioc;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CaloriesTracker
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Application.Configuration.Configurator.Init(Current.Container.Resolve<HttpClient>(), Current.Container.Resolve<Infrastructure.Rest.INativeHttpClientService>());
            Infrastructure.Configuration.Configurator.Init("CaloriesTracker", "1.0.0.0", Device.RuntimePlatform);
            
            await NavigationService.NavigateAsync($"NavigationPage/{nameof(AppEntry)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AppEntry, AppEntryViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
