using System;
using Sample.Navigation;
using System.Threading.Tasks;
using Sample.Mvvm;
using System.Collections.Generic;

namespace Sample.Views
{
    public enum LoginOption
    {
        Login, Signup
    }

    public class LoginViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public List<LabelViewModel<LoginOption>> Options { get; }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Options = new List<LabelViewModel<LoginOption>>
            {
                new LabelViewModel<LoginOption>(LoginOption.Login, "Existing"),
                new LabelViewModel<LoginOption>(LoginOption.Signup, "New")
            };
        }

        public async Task Login()
        {
            await Task.Delay(300);

            await _navigationService.ShowHome();
        }
    }
}
