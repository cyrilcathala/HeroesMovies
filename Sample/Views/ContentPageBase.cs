using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.Extensions.DependencyInjection;
using Sample.Mvvm;

namespace Sample.Views
{
    public class ContentPageBase<TViewModel>: ContentPage where TViewModel : ViewModelBase
    {
        public TViewModel ViewModel => (TViewModel)BindingContext;

        public ContentPageBase()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            SetViewModel();
        }

        private void SetViewModel()
        {
            BindingContext = App.Services.GetRequiredService<TViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.OnDisappearing();
        }
    }
}

