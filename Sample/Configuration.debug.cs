using System;
namespace Sample
{
    public class ConfigurationDebug: Configuration
    {
        // Variables spécifiques à mon environnement de dev
        public override string ServiceUrl => "http://127.0.0.1/api/v1/";

        public override bool UseMockData => true;
        public override bool UseLogin => true;
    }
}
