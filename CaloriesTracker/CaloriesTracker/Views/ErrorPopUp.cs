using CaloriesTracker.CustomViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using XForms.Utils.CustomControls;

namespace CaloriesTracker.Views
{
    public class ErrorPopUp : AwaitablePopUp<bool>, IDisposable
    {
        public ErrorPopUp(string errorDetails) : base(new ErrorView(), DefaultBackgroundColor)
        {
            if (Content is ErrorView)
            {
                (Content as ErrorView).SetErrorDetails(errorDetails);
                (Content as ErrorView).ConfirmEventHandler += OnConfirmEventHandler;
            }
        }

        public void Dispose()
        {
            if (Content is ErrorView)
            {
                (Content as ErrorView).ConfirmEventHandler -= OnConfirmEventHandler;
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
