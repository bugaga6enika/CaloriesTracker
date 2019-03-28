using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XForms.Utils.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonContainer : ContentView
    {
        #region Button container

        public static readonly new BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ButtonContainer), Color.Transparent);

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty BorderRadiusProperty =
           BindableProperty.Create(nameof(BorderRadius), typeof(float), typeof(ButtonContainer), default(float));

        public float BorderRadius
        {
            get => (float)GetValue(BorderRadiusProperty);
            set => SetValue(BorderRadiusProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ButtonContainer), Color.Transparent);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty ButtonContentProperty =
           BindableProperty.Create(nameof(ButtonContent), typeof(View), typeof(ButtonContainer), default(View));

        #endregion

        #region Button content

        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(ButtonWithIconRight), string.Empty);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TitleStyleProperty =
           BindableProperty.Create(nameof(TitleStyle), typeof(Style), typeof(ButtonWithIconRight), default(Style));

        public Style TitleStyle
        {
            get => (Style)GetValue(TitleStyleProperty);
            set => SetValue(TitleStyleProperty, value);
        }

        public static readonly BindableProperty IconStyleProperty =
          BindableProperty.Create(nameof(IconStyle), typeof(Style), typeof(ButtonWithIconRight), default(Style));

        public Style IconStyle
        {
            get => (Style)GetValue(IconStyleProperty);
            set => SetValue(IconStyleProperty, value);
        }

        public static readonly BindableProperty IconProperty =
          BindableProperty.Create(nameof(Icon), typeof(string), typeof(ButtonWithIconRight), string.Empty);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public View ButtonContent
        {
            get => (View)GetValue(ButtonContentProperty);
            set => SetValue(ButtonContentProperty, value);
        }

        #endregion

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonContainer), null, propertyChanged: OnCommandChanged, propertyChanging: OnCommandChanging);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandArgumentProperty =
           BindableProperty.Create(nameof(CommandArgument), typeof(object), typeof(ButtonContainer), null);

        public object CommandArgument
        {
            get => GetValue(CommandArgumentProperty);
            set => SetValue(CommandArgumentProperty, value);
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null && newValue != null)
            {
                (bindable as ButtonContainer).SubscribeToChanges();
            }
        }

        private static void OnCommandChanging(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && newValue == null)
            {
                (bindable as ButtonContainer).UnsubscribeToChanges();
            }
        }

        public ButtonContainer()
        {
            InitializeComponent();
            ButtonContainerFrame.BindingContext = this;
        }

        public void SubscribeToChanges()
        {
            if (Command != null)
            {
                Command.CanExecuteChanged += Command_CanExecuteChanged;
            }
        }

        public void UnsubscribeToChanges()
        {
            if (Command != null)
            {
                Command.CanExecuteChanged -= Command_CanExecuteChanged;
            }
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            if ((sender as ICommand)?.CanExecute(CommandArgument) ?? false)
            {
                ButtonContainerFrame.BackgroundColor = BackgroundColor;
            }
            else
            {
                ButtonContainerFrame.BackgroundColor = Color.Gray;
            }
        }

        private async void ContainerTapped(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandArgument))
            {
                await ButtonContainerFrame.ScaleTo(0.95, 80);
                await ButtonContainerFrame.ScaleTo(1, 80);

                Command.Execute(CommandArgument);
            }
        }
    }
}