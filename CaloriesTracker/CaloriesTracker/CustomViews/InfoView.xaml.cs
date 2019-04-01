using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaloriesTracker.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoView : ContentView
    {
        public EventHandler ConfirmEventHandler { get; set; }

        public InfoView()
        {
            InitializeComponent();
        }

        public void SetInfoDetails(string infoTitle, string infoIcon, string infoDetails)
        {
            TitleLabel.Text = infoTitle;
            IconLabel.Text = infoIcon;
            InfoDescriptionLabel.Text = infoDetails;
        }

        protected void OnConfirmedClicked(object sender, EventArgs e)
        {
            ConfirmEventHandler?.Invoke(sender, e);
        }
    }
}