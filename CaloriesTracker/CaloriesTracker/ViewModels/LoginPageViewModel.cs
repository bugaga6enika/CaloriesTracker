using CaloriesTracker.Application.InternalAuth;
using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Reactive.Linq;
using Xamarin.Forms;
using XForms.Utils.Core;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;

namespace CaloriesTracker.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public ValidatableObject<string> Email { get; private set; }
        public ValidatableObject<string> Password { get; private set; }

        public DelegateCommand OnLoginCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Email = new ValidatableObject<string>();
            Email.Validations.Add(new StringIsNotNullOrEmptyRule());
            Email.Validations.Add(new EmailShouldBeValidRule());

            Password = new ValidatableObject<string>();
            Password.Validations.Add(new StringLengthRule(10));
            Password.Validations.Add(new StringHasDigitRule());
            Password.Validations.Add(new StringHasLowerCaseRule());
            Password.Validations.Add(new StringHasUpperCaseRule());
            Password.Validations.Add(new StringHasUniqueCharactersRule(2));
            Password.Validations.Add(new StringHasNonAlphanumericRule());
            Password.Validations.Add(new StringIsNotNullOrEmptyRule());

            OnLoginCommand = new DelegateCommand(LoginCommandHandler, CanLoginCommandExecute)
                .ObservesProperty(() => Email.Value)
                .ObservesProperty(() => Password.Value)
                .ObservesProperty(() => IsBusy);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Disposables.Add(Email.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Email.Validate(); OnLoginCommand.RaiseCanExecuteChanged(); }));
            Disposables.Add(Password.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Password.Validate(); OnLoginCommand.RaiseCanExecuteChanged(); }));
        }

        private bool CanLoginCommandExecute()
        {
            return !IsBusy && (Email.IsValid && Email.Value?.Length > 0) && (Password.IsValid && Password.Value?.Length > 0);
        }

        private void LoginCommandHandler()
        {
            try
            {
                OnRequestStarted();

                var registrationObservable = Observable.FromAsync(() =>
                {
                    return Mediator.Send(new SendCredentialsCommand(Email.Value, Password.Value, "TODO: pass device id"));
                })
                .Retry(3);

                Disposables.Add(registrationObservable.Subscribe(
                        response => OnTokenReceive(response),
                        (e) =>
                        {
                            OnFailure(e.Message, e);
                            OnRequestEnded();
                        },
                        () =>
                        {
                            OnRequestEnded();
                        }
                    )
                );
            }
            catch (Exception e)
            {
                OnFailure(e.Message, e);
                OnRequestEnded();
            }
        }

        private void OnTokenReceive(AuthToken authToken)
        {
            if (authToken.IsValid)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigationService.NavigateAsync(new Uri($"/NavigationPage/{nameof(MainPage)}", UriKind.Absolute));
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => await ShowErrorAsync("Error", "We couldn't log you in. Please, try again.", TimeSpan.FromSeconds(10)));
            }
        }
    }
}
