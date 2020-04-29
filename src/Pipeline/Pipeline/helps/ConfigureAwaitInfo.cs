using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pipeline.helps
{
    class ConfigureAwaitInfo
    {
        public async Task RunDemo()
        {
            Console.WriteLine($"Thread Information: ThreadId: {Thread.CurrentThread.ManagedThreadId}" +
                $", IsBackground: {Thread.CurrentThread.IsBackground}" +
                $", IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
            await new LineProcessor().ProcessLineAsync();
        }

        private class LineProcessor
        {
            public Task ProcessLineAsync()
            {
                return ProcessLineAsyncInternal();
            }

            private async Task ProcessLineAsyncInternal()
            {
                await Task.Run(() =>
                {
                    Console.WriteLine("Hello World!");
                }).ConfigureAwait(false);

                Console.WriteLine($"Thread Information: ThreadId: {Thread.CurrentThread.ManagedThreadId}" +
                    $", IsBackground: {Thread.CurrentThread.IsBackground}" +
                    $", IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
            }
        }
    }
}
