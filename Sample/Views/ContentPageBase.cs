using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.Extensions.DependencyInjection;
using Sample.Mvvm;
using Xamarin.Forms.PlatformConfiguration;

namespace Sample.Views
{
    public class ContentPageBase<TViewModel>: ContentPageBase where TViewModel : ViewModelBase
    {
        public TViewModel ViewModel => (TViewModel)BindingContext;

        public ContentPageBase()
        {
            SetViewModel();
        }

        private void ContentPageBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //SafeAreaInsets
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

    public class ContentPageBase: ContentPage
    {
        public static Thickness SafeAreaInsets { get; private set; }

        public ContentPageBase()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(false);

            PropertyChanged += ContentPageBase_PropertyChanged;
        }

        private void ContentPageBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SafeAreaInsets")
            {
                SafeAreaInsets = On<iOS>().SafeAreaInsets();
                Xamarin.Forms.Application.Current.Resources["SafeAreaInsets"] = SafeAreaInsets;
            }
        }
    }
}

