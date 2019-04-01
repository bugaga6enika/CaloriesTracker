using System.Threading.Tasks;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public class RegistrationIntroViewModel : RegistrationStepBaseViewModel
    {
        protected override Task<bool> BeforeGoNext()
            => Task.FromResult(true);
    }
}
