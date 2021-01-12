using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public class Logger
    {
        public static void Error(string error)
        {
            Log.Logger.Instance.LogError(error);
            Console.WriteLine(error);
        }

        public static void Debug(string message)
        {
            Log.Logger.Instance.LogDebug(message);
            Console.WriteLine(message);
        }
    }
}
