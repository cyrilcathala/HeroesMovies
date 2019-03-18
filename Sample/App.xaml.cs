using System;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sample.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sample
{
    public partial class App : Application
    {
		public static IServiceProvider Services { get; private set; }

		public App(IServiceCollection serviceCollection)
		{
			InitializeComponent();
            
			Services = serviceCollection
				.AddServices()
				.BuildServiceProvider();

			var navigationService = Services.GetRequiredService<INavigationService>();

			var rootPage = new NavigationPage();
			navigationService.Initialize(rootPage);

			MainPage = rootPage;
		}

		protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
