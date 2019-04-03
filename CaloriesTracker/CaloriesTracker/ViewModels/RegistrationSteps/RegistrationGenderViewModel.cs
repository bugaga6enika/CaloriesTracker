using CaloriesTracker.Application.User.RegistrationSteps.Gender;
using CaloriesTracker.Domain.User;
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
        public ValidatableObject<GenderType> Gender { get; private set; }

        public DelegateCommand<object> OnGenderSelectCommand { get; private set; }

        public RegistrationGenderViewModel()
        {
            Gender = new ValidatableObject<GenderType>();
            Gender.Validations.Add(new EnumShouldBeGreaterThenZeroRule<GenderType>());
            Gender.SetAndValidate(Domain.User.GenderType.NotSpecified);

            OnGenderSelectCommand = new DelegateCommand<object>(gender => OnGenderSelectCommandHandler(gender));

            Disposables.Add(
                Observable.FromAsync(() => Mediator.Send(new GetCurrentGenderQuery()))
                .Subscribe(currentGender => Device.BeginInvokeOnMainThread(() => Gender.SetAndValidate(currentGender)))
            );
        }

        private void OnGenderSelectCommandHandler(object gender)
            => Gender.SetAndValidate((GenderType)gender);

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
