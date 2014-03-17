using System;
using System.Threading.Tasks;
using System.Timers;

namespace LightBus
{
    public static class TaskExt
    {
        private static readonly Lazy<Task> CompletedTask = new Lazy<Task>(() => FromResult<object>(null));

        public static Task FromResult()
        {
            return CompletedTask.Value;
        }

        public static Task<T> FromResult<T>(T result)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(result);
            return tcs.Task;
        }

        public static Task Delay(double milliseconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            var timer = new Timer();
            timer.Elapsed += (obj, args) => tcs.TrySetResult(true);
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }
    }
}