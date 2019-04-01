using CaloriesTracker.Configuration;
using CaloriesTracker.ViewModels.RegistrationSteps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationGenderView : ContentView
    {
        public RegistrationGenderView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext.GetType().FullName != typeof(RegistrationGenderViewModel).FullName)
            {
                BindingContext = ServiceLocator.Current.Resolve<RegistrationGenderViewModel>();
            }

            base.OnBindingContextChanged();
        }
    }
}