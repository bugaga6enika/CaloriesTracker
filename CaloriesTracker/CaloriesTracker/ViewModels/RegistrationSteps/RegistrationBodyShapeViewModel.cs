using CaloriesTracker.Application.InternalAuth.RegistrationSteps.BodyShape;
using CaloriesTracker.Application.InternalAuth.RegistrationSteps.Goal;
using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Models.Registration.Events;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XForms.Utils.Core;
using XForms.Utils.Validation;
using XForms.Utils.Validation.Rules;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public class RegistrationBodyShapeViewModel : RegistrationStepBaseViewModel
    {
        public ValidatableObject<double?> CurrentWeight { get; private set; }
        public ValidatableObject<double?> TargetWeight { get; private set; }
        public ValidatableObject<int?> Height { get; private set; }

        private GoalType _currentGoal;
        public GoalType CurrentGoal
        {
            get => _currentGoal;
            set
            {
                IsTargetWeightVisible = value == GoalType.GainWeight || value == GoalType.LooseWeight;
                SetProperty(ref _currentGoal, value);
            }
        }

        private bool _isTargetWeightVisible;
        public bool IsTargetWeightVisible
        {
            get => _isTargetWeightVisible;
            set => SetProperty(ref _isTargetWeightVisible, value);
        }

        private GoalChangedEvent _goalChangedEvent { get; }

        public RegistrationBodyShapeViewModel()
        {
            _goalChangedEvent = EventAggregator.GetEvent<GoalChangedEvent>();
            _goalChangedEvent.Subscribe(GoalChangedEventHandler);

            CurrentWeight = new ValidatableObject<double?>();
            CurrentWeight.Validations.Add(new DoubleValueShouldBeGreaterThenRule(0));

            TargetWeight = new ValidatableObject<double?>();
            TargetWeight.Validations.Add(new DoubleValueShouldBeGreaterThenRule(0));

            Height = new ValidatableObject<int?>();
            Height.Validations.Add(new IntValueShouldBeGreaterThenRule(0));

            Disposables.Add(
                Observable.FromAsync(() => Mediator.Send(new GetCurrentGoalQuery()))
                .Subscribe(currentGoal => Device.BeginInvokeOnMainThread(() => CurrentGoal = currentGoal))
            );

            Disposables.Add(
                Observable.FromAsync(() => Mediator.Send(new GetCurrentBodyShapeQuery()))
                .Subscribe(async currentBodyShape =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CurrentWeight.Value = currentBodyShape.CurrentWeight;
                        TargetWeight.Value = currentBodyShape.TargetWeight;
                        Height.Value = currentBodyShape.Height;
                    });

                    // Wait > 700 ms to complete the validation
                    await Task.Delay(1000);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CurrentWeight.Errors = new List<string>();
                        CurrentWeight.IsValid = true;
                        TargetWeight.Errors = new List<string>();
                        TargetWeight.IsValid = true;
                        Height.Errors = new List<string>();
                        Height.IsValid = true;
                    });
                })
            );

            Disposables.Add(CurrentWeight.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { CurrentWeight.Validate(); }));
            Disposables.Add(TargetWeight.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { if (CurrentGoal == GoalType.GainWeight || CurrentGoal == GoalType.LooseWeight) { TargetWeight.Validate(); } }));
            Disposables.Add(Height.ToObservable(x => x.Value).Throttle(TimeSpan.FromMilliseconds(700)).Subscribe(x => { Height.Validate(); }));
        }

        private void GoalChangedEventHandler(GoalType goal)
            => Device.BeginInvokeOnMainThread(() => CurrentGoal = goal);

        protected override Task<bool> BeforeGoNext()
        {
            CurrentWeight.Validate();
            Height.Validate();

            var isValid = CurrentWeight.IsValid && Height.IsValid;

            if (CurrentGoal != GoalType.SaveWeight)
            {
                TargetWeight.Validate();

                isValid &= TargetWeight.IsValid;
            }

            if (isValid)
            {
                return Mediator.Send(new SaveBodyShapeCommand(CurrentWeight.Value.Value, TargetWeight.Value, Height.Value.Value, CurrentGoal));
            }

            return Task.FromResult(false);
        }
    }
}
