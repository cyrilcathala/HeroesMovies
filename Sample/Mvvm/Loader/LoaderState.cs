namespace Sample.Mvvm
{
    public enum LoaderState
    {
        NotStarted,
        Loading, // First load
        Refreshing, // Pull to refresh
        Completed,
        Faulted,
        Empty // No content
    }
}
