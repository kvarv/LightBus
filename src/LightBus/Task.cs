using System;
using System.Threading.Tasks;

namespace LightBus
{
    public static class Task
    {
        private static readonly Lazy<System.Threading.Tasks.Task> CompletedTask = new Lazy<System.Threading.Tasks.Task>(() => FromResult<object>(null));

        public static System.Threading.Tasks.Task FromResult()
        {
            return CompletedTask.Value;
        }

        public static Task<T> FromResult<T>(T result)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(result);
            return tcs.Task;
        }

        public static System.Threading.Tasks.Task Delay(double milliseconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            var timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) => tcs.TrySetResult(true);
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }
    }
}