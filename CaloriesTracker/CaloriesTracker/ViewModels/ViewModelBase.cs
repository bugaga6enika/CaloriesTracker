using CaloriesTracker.Configuration;
using CaloriesTracker.Domain.Abstractions.Rest.Exceptions;
using MediatR;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CaloriesTracker.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected CompositeDisposable Disposables;
        protected IMediator Mediator { get; }
        protected IPageDialogService PageDialogService { get; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Mediator = CaloriesTracker.Application.Configuration.IoC.ServiceLocator.Current.Resolve<IMediator>();
            PageDialogService = Configuration.ServiceLocator.Current.Resolve<IPageDialogService>();
            Disposables = new CompositeDisposable();
        }

        /// <summary>
        ///  Called when the implementer has been navigated away from.
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            Disposables.Clear();
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        /// <summary>
        /// Called before the implementor has been navigated to.
        /// </summary>
        /// <param name="parameters">
        /// The navigation parameters.</param>
        /// <remarks>  
        /// Not called when using device hardware or software back buttons
        /// </remarks> 
        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

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

        protected virtual Task ShowErrorAsync(string title, string content, TimeSpan duration)
            => PageDialogService.DisplayAlertAsync(title, content, "OK");

        protected virtual void OnFailure(string message, Exception e)
        {
#if DEBUG
            System.Diagnostics.Debug.Write(e);
#endif

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
    }
}
