using Android.App;
using Android.Content.PM;
using Android.OS;
using CaloriesTracker.Droid.Services;
using CaloriesTracker.Infrastructure.Rest;
using CarouselView.FormsPlugin.Android;
using Prism;
using Prism.Ioc;
using System.Net.Http;

namespace CaloriesTracker.Droid
{
    [Activity(Label = "CaloriesTracker", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CarouselViewRenderer.Init();

            LoadApplication(new App(new AndroidInitializer()));

            XFGloss.Droid.Library.Init(this, bundle);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INativeHttpClientService, NativeHttpClientService>();
            containerRegistry.RegisterInstance(new HttpClient(new HttpClientMiddleware { Timeout = System.TimeSpan.FromSeconds(60) }) { BaseAddress = new System.Uri("http://localhost:44331") });
        }
    }
}