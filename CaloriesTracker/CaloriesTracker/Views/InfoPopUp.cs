using CaloriesTracker.CustomViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using XForms.Utils.CustomControls;

namespace CaloriesTracker.Views
{
    public class InfoPopUp : AwaitablePopUp<bool>, IDisposable
    {
        public InfoPopUp() : base(new InfoView(), DefaultBackgroundColor)
        {
            if (Content is InfoView)
            {
                (Content as InfoView).ConfirmEventHandler += OnConfirmEventHandler;
            }
        }

        public void SetDetails(string title, string icon, string details)
        {
            if (Content is InfoView)
            {
                (Content as InfoView).SetInfoDetails(title, icon, details);
            }
        }

        public void Dispose()
        {
            if (Content is InfoView)
            {
                (Content as InfoView).ConfirmEventHandler -= OnConfirmEventHandler;
            }
        }

        public override async Task<bool> GetResultAsync()
        {
            await PopupNavigation.Instance.PushAsync(this);

            var result = await CompletedTaskSource.Task;

            await PopupNavigation.Instance.PopAsync();

            return result;
        }

        private void OnConfirmEventHandler(object sender, EventArgs e)
        {
            CompletedTaskSource.SetResult(true);
        }
    }
}
