using System;
namespace Sample
{
    public class ConfigurationDebug: Configuration
    {
        public override string ServiceUrl => "http://127.0.0.1/api/v1/";

        public override bool UseMockData => true;
    }
}
