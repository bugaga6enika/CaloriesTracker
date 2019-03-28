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
            ContentPageGloss.SetBackgroundGradient(this, PageSettings.DefaultBackground);
        }
    }
}
