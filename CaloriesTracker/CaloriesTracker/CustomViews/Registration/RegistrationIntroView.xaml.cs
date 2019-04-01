using CaloriesTracker.Configuration;
using CaloriesTracker.ViewModels.RegistrationSteps;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationIntroView : ContentView // RegistrationBaseView
    {
        public RegistrationIntroView()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(async () =>
            {                
                WelcomeLabel.TranslateTo(0, 50, 0);
                DescriptionLabel.TranslateTo(0, 50, 0);                

                await Task.Delay(100);

                WelcomeLabel.TranslateTo(0, 0, PageSettings.AnimationSpeed, Easing.SinIn);
                DescriptionLabel.TranslateTo(0, 0, PageSettings.AnimationSpeed, Easing.SinIn);
                WelcomeLabel.FadeTo(1, PageSettings.AnimationSpeed, Easing.SinIn);
                await DescriptionLabel.FadeTo(1, PageSettings.AnimationSpeed, Easing.SinIn);
            });
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext.GetType().FullName != typeof(RegistrationIntroViewModel).FullName)
            {
                BindingContext = ServiceLocator.Current.Resolve<RegistrationIntroViewModel>();
            }

            base.OnBindingContextChanged();
        }
    }
}