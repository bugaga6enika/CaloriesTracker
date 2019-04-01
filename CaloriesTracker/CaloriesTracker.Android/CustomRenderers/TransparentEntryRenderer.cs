using Android.Content;
using Android.Graphics.Drawables;
using CaloriesTracker.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XForms.Utils.CustomControls;

[assembly: ExportRenderer(typeof(TransparentDatePicker), typeof(TransparentDatePickerRenderer))]
namespace CaloriesTracker.Droid.CustomRenderers
{
    public class TransparentEntryRenderer : EntryRenderer
    {
        public TransparentEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}