using CaloriesTracker.Configuration;
using Xamarin.Forms;
using XFGloss;

namespace CaloriesTracker.Views
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ContentPageGloss.SetBackgroundGradient(this, PageSettings.DefaultBackground);
        }
    }
}
