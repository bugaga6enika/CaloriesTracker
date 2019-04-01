using CaloriesTracker.Configuration;
using CaloriesTracker.ViewModels.RegistrationSteps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationCredentialsView : ContentView
    {
        public RegistrationCredentialsView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext.GetType().FullName != typeof(RegistrationCredentialsViewModel).FullName)
            {
                BindingContext = ServiceLocator.Current.Resolve<RegistrationCredentialsViewModel>();
            }

            base.OnBindingContextChanged();
        }
    }
}