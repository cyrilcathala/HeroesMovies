using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Sample.iOS.Services;
using Sample.Services;
using UIKit;

namespace Sample.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            Xamarin.Forms.Forms.Init();

            XF.Material.iOS.Material.Init();

            var serviceCollection = ConfigureServices();
			LoadApplication(new App(serviceCollection));

            return base.FinishedLaunching(app, options);
        }
        
        private IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IKeyboardService, KeyboardService>();

            return serviceCollection;
        }
    }
}
