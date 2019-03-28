using CaloriesTracker.Application.InternalAuth;
using CaloriesTracker.Domain.Abstractions.Core;
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
    public class RegistrationPageViewModel : ViewModelBase
    {
        public ValidatableObject<string> Email { get; private set; }

        public DelegateCommand RegisterCommand { get; private set; }

        public RegistrationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Email = new ValidatableObject<string>();
            Email.Validations.Add(new StringIsNotNullOrEmptyRule());
            Email.Validations.Add(new EmailShouldBeValidRule());

            RegisterCommand = new DelegateCommand(RegistrationCommandHandler, CanRegistrationCommandExecute)
               .ObservesProperty(() => Email.Value)
               .ObservesProperty(() => IsBusy);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            RegisterCommand.RaiseCanExecuteChanged();

            Disposables.Add(Email.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Email.Validate(); RegisterCommand.RaiseCanExecuteChanged(); }));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            Email.Value = "";
        }

        private bool CanRegistrationCommandExecute()
        {
            return !IsBusy && Email.IsValid && Email.Value?.Length > 0;
        }

        private void RegistrationCommandHandler()
        {
            try
            {
                OnRequestStarted();

                var registrationObservable = Observable.FromAsync(() =>
                {
                    return Mediator.Send(new RegistrationCommand
                    {
                        Email = Email.Value
                    });
                })
                .Retry(3);

                Disposables.Add(registrationObservable.Subscribe(
                        response => OnRegistrationResponse(response),
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

        private void OnRegistrationResponse(OperationResult operationResult)
        {
            if (operationResult.Status != OperationStatus.Success)
            {
                OnFailure("", new AggregateException(operationResult.Exception));
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => await PageDialogService.DisplayAlertAsync("Registration success", "First step of registration is complete. We've sent you an email with instruction to complete the registration", "OK"));
            }
        }
    }
}
