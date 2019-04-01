using CaloriesTracker.Configuration;
using CaloriesTracker.ViewModels.RegistrationSteps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationBodyShapeView : ContentView
    {
        public RegistrationBodyShapeView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext.GetType().FullName != typeof(RegistrationBodyShapeViewModel).FullName)
            {
                BindingContext = ServiceLocator.Current.Resolve<RegistrationBodyShapeViewModel>();
            }

            base.OnBindingContextChanged();
        }
    }
}