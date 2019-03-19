using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Droid
{
    [Activity(Label = "Sample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
           
            global::Xamarin.Forms.Forms.Init(this, bundle);

			var serviceCollection = ConfigureServices();
            LoadApplication(new App(serviceCollection));

#if DEBUG
            XAMLator.Server.PreviewServer.Run();
#endif
        }

        private IServiceCollection ConfigureServices()
		{
			var serviceCollection = new ServiceCollection();

			// TODO : ajouter des dépendances spécifiques platform

			return serviceCollection;
		}
	}
}

