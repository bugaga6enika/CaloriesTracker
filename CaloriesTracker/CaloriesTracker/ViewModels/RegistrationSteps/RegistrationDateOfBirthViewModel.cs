using CaloriesTracker.Application.InternalAuth.RegistrationSteps.DateOfBirth;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public class RegistrationDateOfBirthViewModel : RegistrationStepBaseViewModel
    {
        public ValidatableObject<DateTime> DateOfBirth { get; private set; }

        public RegistrationDateOfBirthViewModel()
        {
            DateOfBirth = new ValidatableObject<DateTime>();
            DateOfBirth.Validations.Add(new DateTimeShouldBeBeforeThenRule(DateTime.Now));

            Disposables.Add(
               Observable.FromAsync(() => Mediator.Send(new GetCurrentDateOfBirthQuery()))
               .Subscribe(currentDateOfBirth => Device.BeginInvokeOnMainThread(() => DateOfBirth.Value = currentDateOfBirth.Date != DateTime.MinValue ? currentDateOfBirth.Date : DateTime.Now.AddYears(-20)))
           );
        }

        protected override Task<bool> BeforeGoNext()
        {
            if (DateOfBirth.IsValid)
            {
                return Mediator.Send(new SaveDateOfBirthCommand(DateOfBirth.Value));
            }

            return Task.FromResult(false);
        }
    }
}
