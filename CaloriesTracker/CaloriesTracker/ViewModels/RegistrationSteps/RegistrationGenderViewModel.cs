using CaloriesTracker.Application.InternalAuth.RegistrationSteps.Gender;
using CaloriesTracker.Domain.InternalAuth;
using Prism.Commands;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public class RegistrationGenderViewModel : RegistrationStepBaseViewModel
    {
        public ValidatableObject<Gender> Gender { get; private set; }

        public DelegateCommand<object> OnGenderSelectCommand { get; private set; }

        public RegistrationGenderViewModel()
        {
            Gender = new ValidatableObject<Gender>();
            Gender.Validations.Add(new EnumShouldBeGreaterThenZeroRule<Gender>());
            Gender.SetAndValidate(Domain.InternalAuth.Gender.NotSpecified);

            OnGenderSelectCommand = new DelegateCommand<object>(gender => OnGenderSelectCommandHandler(gender));

            Disposables.Add(
                Observable.FromAsync(() => Mediator.Send(new GetCurrentGenderQuery()))
                .Subscribe(currentGender => Device.BeginInvokeOnMainThread(() => Gender.SetAndValidate(currentGender)))
            );
        }

        private void OnGenderSelectCommandHandler(object gender)
            => Gender.SetAndValidate((Gender)gender);

        protected override Task<bool> BeforeGoNext()
        {
            if (Gender.IsValid)
            {
                return Mediator.Send(new SaveGenderCommand(Gender.Value));
            }

            return Task.FromResult(false);
        }
    }
}
