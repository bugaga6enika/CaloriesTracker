using CaloriesTracker.Configuration;
using CaloriesTracker.Domain.Abstractions.Rest.Exceptions;
using CaloriesTracker.Models.Registration.Events;
using CaloriesTracker.Views;
using MediatR;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CaloriesTracker.ViewModels.RegistrationSteps
{
    public abstract class RegistrationStepBaseViewModel : BindableBase, IDisposable
    {
        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

        public DelegateCommand OnGoBackCommand { get; private set; }
        public DelegateCommand OnGoNextCommand { get; private set; }

        protected IEventAggregator EventAggregator { get; private set; }
        protected IMediator Mediator { get; private set; }
        protected CompositeDisposable Disposables;
        protected IPageDialogService PageDialogService { get; }

        private GoPreviousRegistrationStepEvent _goPreviousRegistrationStepEvent { get; set; }
        private GoNextRegistrationStepEvent _goNextRegistrationStepEvent { get; set; }

        public RegistrationStepBaseViewModel()
        {
            EventAggregator = ServiceLocator.Current.Resolve<IEventAggregator>();
            Mediator = Application.Configuration.IoC.ServiceLocator.Current.Resolve<IMediator>();
            Disposables = new CompositeDisposable();
            PageDialogService = ServiceLocator.Current.Resolve<IPageDialogService>();

            _goPreviousRegistrationStepEvent = EventAggregator.GetEvent<GoPreviousRegistrationStepEvent>();
            _goNextRegistrationStepEvent = EventAggregator.GetEvent<GoNextRegistrationStepEvent>();

            OnGoBackCommand = new DelegateCommand(OnGoBackCommandHandler);
            OnGoNextCommand = new DelegateCommand(async () => await OnGoNextCommandHandler());
        }

        protected abstract Task<bool> BeforeGoNext();

        private async Task OnGoNextCommandHandler()
        {
            Device.BeginInvokeOnMainThread(() => IsBusy = true);

            if (await BeforeGoNext().ConfigureAwait(false))
            {
                _goNextRegistrationStepEvent.Publish();
            }

            Device.BeginInvokeOnMainThread(() => IsBusy = false);
        }

        private void OnGoBackCommandHandler()
            => _goPreviousRegistrationStepEvent.Publish();

        protected void OnRequestStarted()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = true;
            });
        }

        protected void OnRequestEnded()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
            });
        }

        protected virtual async Task ShowErrorAsync(string title, string content, TimeSpan duration)
        {
            using (var errorPopUp = new ErrorPopUp())
            {
                errorPopUp.SetDetails(content);
                await errorPopUp.GetResultAsync();
            }
        }

        protected virtual async Task ShowInfoAsync(string title, string icon, string content)
        {
            using (var infoPopUp = new InfoPopUp())
            {
                infoPopUp.SetDetails(title, icon, content);
                await infoPopUp.GetResultAsync();
            }
        }

        protected virtual void OnFailure(string message, Exception e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (e is UnauthorizedException)
                {
                    await ShowErrorAsync("Authorization error", "Sorry, dear user", TimeSpan.FromSeconds(5));
                }
                else
                if (e is NotFoundException)
                {
                    await ShowErrorAsync("Resource error", "Sorry, resource not found", TimeSpan.FromSeconds(5));
                }
                else
                if (e is ForbiddenException)
                {
                    await ShowErrorAsync("Access error", "Sorry, resource is forbidden", TimeSpan.FromSeconds(5));
                }
                else
                if (e is BadRequestException)
                {
                    await ShowErrorAsync("Request error", "Sorry, bad request", TimeSpan.FromSeconds(5));
                }
                else
                if (e is InternalServerErrorException)
                {
                    await ShowErrorAsync("Request error", "Sorry, bad request", TimeSpan.FromSeconds(5));
                }
                else
                if (e is FluentValidation.ValidationException validationException)
                {
                    await ShowErrorAsync("Validation error", validationException.Errors.Aggregate("", (acc, current) => $"{acc}{current.ErrorMessage} for {current.PropertyName}\n"), TimeSpan.FromSeconds(5));
                }
                else if (e is AggregateException aggregateException)
                {
                    await ShowErrorAsync("Error", aggregateException.InnerExceptions.Aggregate("", (acc, current) =>
                    {
                        if (current is Domain.Abstractions.Validation.ValidationException vE)
                        {
                            acc = $"{acc}\n{vE.Message} for {vE.ParamName}";
                        }
                        else
                        {
                            acc = $"{acc}\n{current.Message}";
                        }

                        return acc;
                    }), TimeSpan.FromSeconds(5));
                }
                else
                {
                    await ShowErrorAsync("Error", "Sorry, error occured", TimeSpan.FromSeconds(5));
                }
            });
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disposables.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RegistrationStepBaseViewModel() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
