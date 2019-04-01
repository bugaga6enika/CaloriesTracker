using CaloriesTracker.Configuration;
using Xamarin.Forms;
using XFGloss;

namespace CaloriesTracker.Views
{
    public partial class AppEntry : ContentPage
    {
        public AppEntry()
        {
            try
            {
                InitializeComponent();
            }
            catch (System.Exception e)
            {

                throw;
            }

            NavigationPage.SetHasNavigationBar(this, false);

            ContentPageGloss.SetBackgroundGradient(this, PageSettings.DefaultBackground);           

            Device.BeginInvokeOnMainThread(async () =>
            {
                WelcomeLabel.TranslateTo(0, 50, 0);
                DescriptionLabel.TranslateTo(0, 50, 0);
                WelcomeLabel.FadeTo(0, 100);
                await DescriptionLabel.FadeTo(0, 100);
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(async () =>
            {
                WelcomeLabel.TranslateTo(0, 0, PageSettings.AnimationSpeed, Easing.SinIn);
                DescriptionLabel.TranslateTo(0, 0, PageSettings.AnimationSpeed, Easing.SinIn);
                WelcomeLabel.FadeTo(1, PageSettings.AnimationSpeed, Easing.SinIn);
                await DescriptionLabel.FadeTo(1, PageSettings.AnimationSpeed, Easing.SinIn);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Device.BeginInvokeOnMainThread(async () =>
            {
                WelcomeLabel.FadeTo(0, 100);
                await DescriptionLabel.FadeTo(0, 100);
                WelcomeLabel.TranslateTo(0, 50, 0);
                DescriptionLabel.TranslateTo(0, 50, 0);
            });
        }        
    }
}