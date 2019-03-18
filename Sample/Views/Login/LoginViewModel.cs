using System;
using Sample.Navigation;
using System.Threading.Tasks;
using Sample.Mvvm;

namespace Sample.Views
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task Login()
        {
            await Task.Delay(300);

            await _navigationService.ShowHome();
        }
    }
}
