using CaloriesTracker.Application.User;
using CaloriesTracker.Commands;
using CaloriesTracker.Domain.User;
using CaloriesTracker.Models;
using CaloriesTracker.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace CaloriesTracker.ViewModels
{
    public class AppEntryViewModel : ViewModelBase
    {
        public DelegateCommand RegisterCommand { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand ShowSearchPanelCommand { get; private set; }

        public AppEntryViewModel(INavigationService navigationService) : base(navigationService)
        {
            RegisterCommand = new CustomDelegateCommand(RegisterCommandHandler, RegisterCanExecute);
            LoginCommand = new CustomDelegateCommand(LoginCommandHandler, LoginCanExecute);
            ShowSearchPanelCommand = new DelegateCommand(ShowSearchPanelCommandHandler);            
        }

        private void ShowSearchPanelCommandHandler()
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            LoginCommand.IsActive = true;
            RegisterCommand.IsActive = true;

            CheckCurrentAuthToken();
        }

        private bool LoginCanExecute()
            => LoginCommand.IsActive;

        private bool RegisterCanExecute()
            => RegisterCommand.IsActive;

        private void CheckCurrentAuthToken()
        {
            var observable = Observable.FromAsync(() => Mediator.Send<AuthToken>(new GetCurrentAuthTokenQuery()))
                .Retry(3)
                .Catch((Exception e) => Observable.Return(AuthToken.Empty));

            Disposables.Add(observable.Subscribe(
                authToken => OnCurrentTokenReceived(authToken),
                (Exception e) => OnFailure(e.Message, e),
                () => OnRequestEnded()));
        }

        private void OnCurrentTokenReceived(AuthToken authToken)
        {
            if (authToken.IsValid)
            {
                Device.BeginInvokeOnMainThread(async () => await NavigationService.NavigateAsync(nameof(MainPage)));
            }            
        }

        private void LoginCommandHandler()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                LoginCommand.IsActive = false;
                await NavigationService.NavigateAsync(nameof(LoginPage));
            });
        }

        private void RegisterCommandHandler()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                RegisterCommand.IsActive = false;
                await NavigationService.NavigateAsync(nameof(RegistrationPage));
            });
        }
    }
}
