using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using XForms.Utils.Core;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;

namespace CaloriesTracker.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
        public ValidatableObject<string> Username { get; private set; }
        public ValidatableObject<string> Password { get; private set; }

        public DelegateCommand LoginCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Username = new ValidatableObject<string>();
            Username.Validations.Add(new StringIsNotNullOrEmptyRule());
            //Username.Validations.Add(new EmailRule());

            Password = new ValidatableObject<string>();
            Password.Validations.Add(new StringLengthRule(10));
            Password.Validations.Add(new StringHasDigitRule());
            Password.Validations.Add(new StringHasLowerCaseRule());
            Password.Validations.Add(new StringHasUpperCaseRule());
            Password.Validations.Add(new StringHasUniqueCharactersRule(2));
            Password.Validations.Add(new StringHasNonAlphanumericRule());
            Password.Validations.Add(new StringIsNotNullOrEmptyRule());

            LoginCommand = new DelegateCommand(async () => await LoginCommandHandler(), CanLoginCommandExecute)
                .ObservesProperty(() => Username.Value)
                .ObservesProperty(() => Password.Value)
                .ObservesProperty(() => IsBusy);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Disposables.Add(Username.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Username.Validate(); LoginCommand.RaiseCanExecuteChanged(); }));
            Disposables.Add(Password.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Password.Validate(); LoginCommand.RaiseCanExecuteChanged(); }));
        }

        private bool CanLoginCommandExecute()
        {
            return !IsBusy && (Username.IsValid && Username.Value?.Length > 0) && (Password.IsValid && Password.Value?.Length > 0);
        }

        private Task LoginCommandHandler()
        {
            return Task.CompletedTask;
            //try
            //{
            //    IsBusy = true;
            //    if (!Username.IsValid || !Password.IsValid)
            //    {
            //        return;
            //    }

            //    OnRequestStarted();

            //    try
            //    {
            //        var deviceId = await _deviceInfoService.GetDeviceId();
            //        var Token = await Mediator.Send(new SendCredentialsCommand
            //        {
            //            Username = Username.Value,
            //            Password = Password.Value,
            //            DeviceId = deviceId
            //        });
            //        await OnTokenReceived(Token);
            //    }
            //    catch (Exception e)
            //    {
            //        OnFailure(e.Message, e);
            //    }
            //    finally
            //    {
            //        OnRequestEnded();
            //        IsBusy = false;
            //    }


            //    //Disposables.Add(
            //    //    Observable.FromAsync(_deviceInfoService.GetDeviceId)
            //    //    .Subscribe(deviceId =>
            //    //    {
            //    //        if (!string.IsNullOrWhiteSpace(deviceId))
            //    //        {
            //    //            var loginObservable = Observable.FromAsync(() =>
            //    //            {
            //    //                return Mediator.Send(new SendCredentialsCommand
            //    //                {
            //    //                    Username = Username.Value,
            //    //                    Password = Password.Value,
            //    //                    DeviceId = deviceId
            //    //                });
            //    //            })
            //    //            .Retry(3);

            //    //            Disposables.Add(loginObservable.Subscribe(
            //    //                    async (token) =>await OnAuthTokenReceived(token),
            //    //                    (e) =>
            //    //                    {
            //    //                        OnFailure(e.Message, e);
            //    //                        OnRequestEnded();
            //    //                    },
            //    //                    () =>
            //    //                    {
            //    //                        OnRequestEnded();
            //    //                    }
            //    //                )
            //    //            );
            //    //        }
            //    //    },
            //    //    (e) =>
            //    //    {
            //    //        OnFailure(e.Message, e);
            //    //        OnRequestEnded();
            //    //    },
            //    //    () => OnRequestEnded())
            //    //);
            //}
            //catch (Exception e)
            //{
            //    OnFailure(e.Message, e);
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        IsBusy = false;
            //    });
            //}
        }        
    }
}
