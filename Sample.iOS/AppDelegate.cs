using Foundation;
using Microsoft.Extensions.DependencyInjection;
using UIKit;

namespace Sample.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();

			var serviceCollection = ConfigureServices();
			LoadApplication(new App(serviceCollection));

#if DEBUG
            XAMLator.Server.PreviewServer.Run();
#endif

            return base.FinishedLaunching(app, options);
        }
        
        private IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // TODO : ajouter des dépendances spécifiques platform

            return serviceCollection;
        }
    }
}
