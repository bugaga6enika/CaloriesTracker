using CaloriesTracker.Configuration;
using Xamarin.Forms;
using XFGloss;

namespace CaloriesTracker.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ContentPageGloss.SetBackgroundGradient(this, PageSettings.DefaultBackground);
        }
    }
}
