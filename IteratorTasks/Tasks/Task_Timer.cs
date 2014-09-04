﻿using System.Threading;

namespace IteratorTasks
{
    public partial class Task
    {
        public static Task Delay(int delayMilliseconds, TaskScheduler scheduler)
        {
            return Delay(delayMilliseconds, CancellationToken.None, scheduler);
        }

        public static Task Delay(int delayMilliseconds, CancellationToken ct, TaskScheduler scheduler)
        {
            var tcs = new TaskCompletionSource<object>(scheduler);

            Timer timer = null;

            timer = new Timer(state =>
            {
                timer.Dispose();
                tcs.SetResult(null);
            }, null, delayMilliseconds, Timeout.Infinite);

            if (ct != CancellationToken.None)
                ct.Register(() => timer.Dispose());

            return tcs.Task;
        }
    }
}
