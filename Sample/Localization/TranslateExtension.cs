using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Localization
{
    [ContentProperty("Text")]
    public class TranslateExtension: IMarkupExtension
    {
        private const string ResourceId = "Strings";

        private static readonly Lazy<ResourceManager> ResourceManager = new Lazy<ResourceManager>(GetResourceManager);

        public string Text { get; set; }

        public TranslateExtension()
        {
        }

        private static ResourceManager GetResourceManager()
        {
            var assembly = typeof(TranslateExtension).GetTypeInfo().Assembly;
            var assemblyName = assembly.GetName();
            return new ResourceManager($"{assemblyName.Name}.{ResourceId}", assembly);
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            var translation = ResourceManager.Value.GetString(Text);

            if (translation == null)
            {
                translation = Text;
            }

            return translation;
        }
    }
}
