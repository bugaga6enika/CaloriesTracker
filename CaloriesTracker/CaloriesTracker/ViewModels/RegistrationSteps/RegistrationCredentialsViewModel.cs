using CaloriesTracker.Application.InternalAuth.RegistrationSteps.Credentials;
using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using Prism.Commands;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;
using XForms.Utils.Core;
using CaloriesTracker.Models.Registration.Events;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public class RegistrationCredentialsViewModel : RegistrationStepBaseViewModel
    {
        public ValidatableObject<string> Email { get; private set; }
        public ValidatableObject<string> Password { get; private set; }

        private RegistrationInfo _registrationInfo;
        private RegistrationCompletedEvent _registrationCompletedEvent { get; set; }

        public DelegateCommand OnRegisterCommand { get; private set; }

        public RegistrationCredentialsViewModel()
        {
            _registrationCompletedEvent = EventAggregator.GetEvent<RegistrationCompletedEvent>();

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

            OnRegisterCommand = new DelegateCommand(OnRegistrationCommandHandler, CanRegistrationCommandExecute)
               .ObservesProperty(() => Email.Value)
               .ObservesProperty(() => Password.Value)
               .ObservesProperty(() => IsBusy);

            Disposables.Add(
              Observable.FromAsync(() => Mediator.Send(new GetRegistrationInfoQuery()))
              .Subscribe(registrationInfo => _registrationInfo = registrationInfo)
            );

            Disposables.Add(Email.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Email.Validate(); OnRegisterCommand.RaiseCanExecuteChanged(); }));
            Disposables.Add(Password.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Password.Validate(); OnRegisterCommand.RaiseCanExecuteChanged(); }));
        }

        private bool CanRegistrationCommandExecute()
        {
            return !IsBusy && Email.IsValid && Email.Value?.Length > 0 && Password.IsValid && Password.Value?.Length > 0;
        }

        private void OnRegistrationCommandHandler()
        {
            try
            {
                OnRequestStarted();

                var registrationObservable = Observable.FromAsync(() =>
                {
                    return Mediator.Send(new RegistrationCommand(Email.Value,
                        Password.Value,
                        _registrationInfo.Goal,
                        _registrationInfo.Gender,
                        _registrationInfo.CurrentWeight,
                        _registrationInfo.TargetWeight,
                        _registrationInfo.Height,
                        _registrationInfo.DateOfBirth));
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
                _registrationCompletedEvent.Publish();
            }
        }


        protected override Task<bool> BeforeGoNext()
        {
            throw new NotImplementedException();
        }
    }
}
