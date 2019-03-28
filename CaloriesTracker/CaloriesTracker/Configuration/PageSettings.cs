using Xamarin.Forms;
using XFGloss;

namespace CaloriesTracker.Configuration
{
    internal class PageSettings
    {
        public const int AnimationSpeed = 550;

        public static Gradient DefaultBackground => new Gradient()
        {
            Rotation = 135,
            Steps = new GradientStepCollection()
                {
                    new GradientStep((Color)App.Current.Resources["BackgroundColorStep1"], 0),
                    new GradientStep((Color)App.Current.Resources["BackgroundColorStep2"], 0.2),
                    new GradientStep((Color)App.Current.Resources["BackgroundColorStep3"], 0.4),
                    new GradientStep((Color)App.Current.Resources["BackgroundColorStep4"], 0.6),
                    new GradientStep((Color)App.Current.Resources["BackgroundColorStep5"], 0.8),
                    new GradientStep((Color)App.Current.Resources["BackgroundColorStep6"], 1)
                }
        };
    }
}
