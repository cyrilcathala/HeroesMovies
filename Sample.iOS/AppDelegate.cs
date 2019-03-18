using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Extensions.DependencyInjection;
using UIKit;

namespace Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

			var serviceCollection = ConfigureServices();
			LoadApplication(new App(serviceCollection));

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
