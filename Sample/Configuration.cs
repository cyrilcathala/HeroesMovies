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
        // Mandatory configuration by environment tagged abstract
        public abstract string ServiceUrl { get; }

        // Facultative config with default values tagged virtual
        public virtual bool UseLogin => true;
        public virtual bool UseMockData => false;
    }
}
