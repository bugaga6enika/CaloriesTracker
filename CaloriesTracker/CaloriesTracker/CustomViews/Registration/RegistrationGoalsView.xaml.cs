using CaloriesTracker.Configuration;
using CaloriesTracker.ViewModels.RegistrationSteps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationGoalsView : ContentView
    {
        public RegistrationGoalsView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext.GetType().FullName != typeof(RegistrationGoalsViewModel).FullName)
            {
                BindingContext = ServiceLocator.Current.Resolve<RegistrationGoalsViewModel>();
            }

            base.OnBindingContextChanged();
        }
    }
}