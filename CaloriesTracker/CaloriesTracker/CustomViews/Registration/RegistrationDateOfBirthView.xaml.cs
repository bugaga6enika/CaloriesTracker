using CaloriesTracker.Configuration;
using CaloriesTracker.ViewModels.RegistrationSteps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationDateOfBirthView : ContentView
    {
        public RegistrationDateOfBirthView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext.GetType().FullName != typeof(RegistrationDateOfBirthViewModel).FullName)
            {
                BindingContext = ServiceLocator.Current.Resolve<RegistrationDateOfBirthViewModel>();
            }

            base.OnBindingContextChanged();
        }
    }
}