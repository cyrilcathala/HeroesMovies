namespace Sample
{
    public interface IConfiguration
    {
        string ServiceUrl { get; }

        bool UseLogin { get; }
        bool UseMockData { get; }
    }

    public abstract class Configuration: IConfiguration
    {
        // Configuration obligatoire par environnement
        public abstract string ServiceUrl { get; }

        // Configuration facultative avec valeur par défaut à true
        public virtual bool UseLogin => true;
        public virtual bool UseMockData => false;
    }
}
