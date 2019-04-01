using CaloriesTracker.Configuration;
using Xamarin.Forms;
using XFGloss;

namespace CaloriesTracker.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ContentPageGloss.SetBackgroundGradient(this, PageSettings.DefaultBackground);
        }
    }
}