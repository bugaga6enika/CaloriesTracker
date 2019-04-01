using Android.Content;
using Android.Graphics.Drawables;
using CaloriesTracker.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XForms.Utils.CustomControls;

[assembly: ExportRenderer(typeof(TransparentEntry), typeof(TransparentEntryRenderer))]
namespace CaloriesTracker.Droid.CustomRenderers
{
    public class TransparentDatePickerRenderer : DatePickerRenderer
    {
        public TransparentDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}