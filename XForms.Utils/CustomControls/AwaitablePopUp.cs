using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XForms.Utils.CustomControls
{
    public abstract class AwaitablePopUp<T> : PopupPage
    {
        public static Color DefaultBackgroundColor = new Color(0, 0, 0, 0.4);

        protected Task<T> PageClosedTask { get { return CompletedTaskSource.Task; } }

        protected TaskCompletionSource<T> CompletedTaskSource { get; set; }

        public AwaitablePopUp(View contentBody, Color backgroudColor)
        {
            Content = contentBody;

            CompletedTaskSource = new TaskCompletionSource<T>();

            BackgroundColor = backgroudColor;
        }

        public abstract Task<T> GetResultAsync();

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}
