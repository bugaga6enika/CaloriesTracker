using CaloriesTracker.Models;
using CaloriesTracker.Models.Registration.Events;
using CaloriesTracker.Views;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CaloriesTracker.ViewModels
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        public ObservableCollection<RegistrationStepModel> RegistrationSteps { get; private set; }

        private int _position;
        public int Position { get => _position; set => SetProperty(ref _position, value); }

        private GoPreviousRegistrationStepEvent _goPreviousRegistrationStepEvent { get; set; }
        private GoNextRegistrationStepEvent _goNextRegistrationStepEvent { get; set; }
        private RegistrationCompletedEvent _registrationCompletedEvent { get; set; }

        public RegistrationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Position = 0;
            RegistrationSteps = new ObservableCollection<RegistrationStepModel>
            {
                new RegistrationStepModel { TypeOfView = RegistrationStepModel.Type.Intro },
                new RegistrationStepModel { TypeOfView = RegistrationStepModel.Type.Goals },
                new RegistrationStepModel { TypeOfView = RegistrationStepModel.Type.Gender },
                new RegistrationStepModel { TypeOfView = RegistrationStepModel.Type.BodyShape },
                new RegistrationStepModel { TypeOfView = RegistrationStepModel.Type.DateOfBirth },
                new RegistrationStepModel { TypeOfView = RegistrationStepModel.Type.Credentials }
            };

            _goPreviousRegistrationStepEvent = EventAggregator.GetEvent<GoPreviousRegistrationStepEvent>();
            _goNextRegistrationStepEvent = EventAggregator.GetEvent<GoNextRegistrationStepEvent>();
            _registrationCompletedEvent = EventAggregator.GetEvent<RegistrationCompletedEvent>();

            _goPreviousRegistrationStepEvent.Subscribe(GoPreviousRegistrationStepEventHandler, Prism.Events.ThreadOption.UIThread, false);
            _goNextRegistrationStepEvent.Subscribe(GoNextRegistrationStepEventHandler, Prism.Events.ThreadOption.UIThread, false);
            _registrationCompletedEvent.Subscribe(RegistrationCompletedEventHandler, false);
        }

        private void RegistrationCompletedEventHandler()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavigationService.NavigateAsync(new Uri($"/NavigationPage/{nameof(MainPage)}", UriKind.Absolute));
            });
        }

        private void GoNextRegistrationStepEventHandler()
        {
            if (Position < RegistrationSteps.Count)
            {
                Position++;
            }
        }

        private void GoPreviousRegistrationStepEventHandler()
        {
            if (Position > 0)
            {
                Position--;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            _goPreviousRegistrationStepEvent.Unsubscribe(GoPreviousRegistrationStepEventHandler);
            _goNextRegistrationStepEvent.Unsubscribe(GoNextRegistrationStepEventHandler);
            _registrationCompletedEvent.Unsubscribe(RegistrationCompletedEventHandler);
        }
    }
}
