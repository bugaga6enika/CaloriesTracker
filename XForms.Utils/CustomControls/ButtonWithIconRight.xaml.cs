using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XForms.Utils.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonWithIconRight : ContentView
    {
        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(ButtonWithIconRight), string.Empty);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty IconProperty =
          BindableProperty.Create(nameof(Icon), typeof(string), typeof(ButtonWithIconRight), string.Empty);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public ButtonWithIconRight()
        {
            InitializeComponent();
        }
    }
}