using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Helpers
{
    public class TaskRunner
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        /// <summary>
        /// Executes a task with a given token which is cancelled with <see cref="CancelExecutions"/>
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="safeExecution">If true, it won't throw any exception and wrap them silently</param>
        public async Task ExecuteAsync(Func<CancellationToken, Task> action, bool safeExecution = false)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();
                await action(CancellationToken);
            }
            catch (AggregateException e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                if (!safeExecution)
                    throw;
            }
            catch (Exception)
            {
                if (!safeExecution)
                    throw;
            }
        }

        /// <summary>
        /// Executes a task with a given token which is cancelled with <see cref="CancelExecutions"/>
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="safeExecution">If true, it won't throw any exception and wrap them silently</param>
        public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, bool safeExecution = false)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();
                return await action(CancellationToken);
            }
            catch (AggregateException e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                if (!safeExecution)
                    throw;
            }
            catch (Exception)
            {
                if (!safeExecution)
                    throw;
            }

            return default(T);
        }

        public void CancelExecutions()
        {
            if (!_cancellationTokenSource.IsCancellationRequested && CancellationToken.CanBeCanceled)
                _cancellationTokenSource.Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
