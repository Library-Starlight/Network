using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sockets.Business
{
    public class Transcode
    {
        public static void Start(string[] args)
        {
            try
            {
                if (args.Contains("-c"))
                    new Thread(async () => await new TranscodeClient().StartAsync()).Start();
                if (args.Contains("-s"))
                    new Thread(async () => await new TranscodeServer().StartAsync()).Start();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
