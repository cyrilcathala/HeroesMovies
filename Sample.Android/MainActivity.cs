using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using Sample.Droid.Services;
using Sample.Services;

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

            Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            Xamarin.Forms.Forms.Init(this, bundle);

            XF.Material.Droid.Material.Init(this, bundle);

            var serviceCollection = ConfigureServices();
            LoadApplication(new App(serviceCollection));
        }

        private IServiceCollection ConfigureServices()
		{
			var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IKeyboardService, KeyboardService>();

			return serviceCollection;
		}
	}
}

