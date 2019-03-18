using System;
using System.Threading.Tasks;
using Sample.Mvvm;
using Sample.Navigation;

namespace Sample.Views
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
        }

        public override Task OnAppearing()
        {
            return base.OnAppearing();
        }

        public override Task OnDisappearing()
        {
            return base.OnDisappearing();
        }
    }
}
