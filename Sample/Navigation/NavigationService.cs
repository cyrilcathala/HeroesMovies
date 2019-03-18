using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sample.Views;
using System.Threading;

namespace Sample.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IConfiguration _configuration;

        private NavigationPage _rootPage;
        public INavigation Navigation => _rootPage.Navigation;

        public NavigationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(NavigationPage navigationPage)
        {
            _rootPage = navigationPage;
            _rootPage.Pushed += RootPage_Pushed;

            if (_configuration.UseLogin)
            {
                ShowLogin();
            }
            else
            {
                ShowHome();
            }
        }

        private void RootPage_Pushed(object sender, NavigationEventArgs e)
        {
            Console.WriteLine($"Navigated to: {e.Page.GetType().Name}");
        }

        public Task ShowLogin()
        {
            return Navigation.PushAndClearAsync(new LoginPage());
        }

        public Task ShowHome()
        {
            return Navigation.PushAndClearAsync(new HomePage());
        }
    }
}
