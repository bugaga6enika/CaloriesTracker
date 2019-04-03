using CaloriesTracker.Application.User.RegistrationSteps.Goal;
using CaloriesTracker.Domain.User;
using CaloriesTracker.Models.Registration.Events;
using Prism.Commands;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public class RegistrationGoalsViewModel : RegistrationStepBaseViewModel
    {
        public ValidatableObject<GoalType> Goal { get; }
        public DelegateCommand<object> OnGoalSelectCommand { get; }

        private GoalChangedEvent _goalChangedEvent { get; }

        public RegistrationGoalsViewModel()
        {
            _goalChangedEvent = EventAggregator.GetEvent<GoalChangedEvent>();

            Goal = new ValidatableObject<GoalType>();
            Goal.Validations.Add(new EnumShouldBeGreaterThenZeroRule<GoalType>());
            Goal.SetAndValidate(GoalType.NotSpecified);

            OnGoalSelectCommand = new DelegateCommand<object>(goalType => OnGoalSelectCommandHandler(goalType));

            Disposables.Add(
                Observable.FromAsync(() => Mediator.Send(new GetCurrentGoalQuery()))
                .Subscribe(currentGoal => Device.BeginInvokeOnMainThread(() => Goal.SetAndValidate(currentGoal)))
            );
        }

        private void OnGoalSelectCommandHandler(object goalType)
        {
            var goal = (GoalType)goalType;
            Goal.SetAndValidate(goal);
            _goalChangedEvent.Publish(goal);
        }

        protected override Task<bool> BeforeGoNext()
        {
            if (Goal.IsValid)
            {
                return Mediator.Send(new SaveGoalCommand(Goal.Value));
            }

            return Task.FromResult(false);
        }
    }
}
