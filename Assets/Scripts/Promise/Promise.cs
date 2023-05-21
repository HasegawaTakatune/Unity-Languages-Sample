using System;
using System.Threading.Tasks;

namespace Promise
{
    public class Promise<T>
    {
        private TaskCompletionSource<T> tcs;

        public Promise()
        {
            tcs = new TaskCompletionSource<T>();
        }

        public Task<T> Task => tcs.Task;

        public void Resolve(T result)
        {
            tcs.SetResult(result);
        }

        public void Reject(Exception ex)
        {
            tcs.SetException(ex);
        }
    }

}