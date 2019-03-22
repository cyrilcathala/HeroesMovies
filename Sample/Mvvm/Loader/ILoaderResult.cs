namespace Sample.Mvvm
{
    public interface ILoaderResult
    {
        bool IsEmpty { get; }
        bool IsSuccess { get; }
        string ErrorMessage { get; }
        string EmptyMessage { get; }
    }
}
