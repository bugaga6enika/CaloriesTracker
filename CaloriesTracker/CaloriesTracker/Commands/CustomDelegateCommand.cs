using System;
using Prism.Commands;

namespace CaloriesTracker.Commands
{
    internal class CustomDelegateCommand : DelegateCommand
    {
        public CustomDelegateCommand(Action executeMethod) : base(executeMethod)
        {
        }

        public CustomDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        protected override void OnIsActiveChanged()
        {
            base.OnIsActiveChanged();
            RaiseCanExecuteChanged();
        }
    }
}
