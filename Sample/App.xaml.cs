using System;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms.Xaml;
using Sample.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sample
{
    public partial class App
    {
        public static IServiceProvider Services { get; private set; }

        public App(IServiceCollection serviceCollection)
        {
            InitializeComponent();

            XF.Material.Forms.Material.Init(this, "Material.Configuration");
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.JamIconsModule())
                                  .With(new Plugin.Iconize.Fonts.MaterialDesignIconsModule());

            Services = serviceCollection
                .AddServices()
                .BuildServiceProvider();

            var navigationService = Services.GetRequiredService<INavigationService>();
            navigationService.Initialize();
        }
    }
}
