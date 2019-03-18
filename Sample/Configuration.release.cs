using System;
namespace Sample
{
    public class ConfigurationRelease : Configuration
    {
        public ConfigurationRelease()
        {
            // Parsing dans le constructeur pour faire qu'1 fois et que ça crash au démarrage si la valeur est mal initialisée
            _useLogin = bool.Parse("#{UseLogin}#");
        }

        // Variable injectée par la CI (token par défaut de la task "Replace Tokens" de VSTS)
        public override string ServiceUrl => "#{Environment}#";

        private bool _useLogin;
        public override bool UseLogin => _useLogin;
    }
}
