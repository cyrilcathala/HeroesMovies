using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sample.Mvvm
{
    public interface ILoaderViewModel : INotifyPropertyChanged
    {
        string EmptyMessage { get; }
        string ErrorMessage { get; }
        LoaderState State { get; set; }
        bool IsBusy { get; }
        bool IsCompleted { get; }
        ICommand RefreshCommand { get; }

        void CancelExecutions();
        Task Execute(Func<CancellationToken, Task<ILoaderResult>> loadTask, bool cancelPreviousTasks = true);
        Task Retry();
    }
}