using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorView : ContentView
    {
        public EventHandler ConfirmEventHandler { get; set; }

        public ErrorView()
        {
            InitializeComponent();
        }

        public void SetErrorDetails(string errorDetails)
        {
            errorLabel.Text = errorDetails;
        }

        protected void OnConfirmedClicked(object sender, EventArgs e)
        {
            ConfirmEventHandler?.Invoke(sender, e);
        }
    }
}