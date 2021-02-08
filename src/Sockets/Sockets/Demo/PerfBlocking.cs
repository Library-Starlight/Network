using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sockets.Demo
{
    public static class PerfBlocking
    {
        public static void HardwareInfo()
        {
            // var processBits = Environment.Is64BitProcess ? "x64" : "x86";
            // System.Console.WriteLine($"    处理器位数：{processBits}");
            // // System.Console.WriteLine($"逻辑处理器个数：{Environment.ProcessorCount}");
            // System.Console.WriteLine($"物理处理器个数：{Environment.ProcessorCount}");

            // var processorId = System.Threading.Thread.GetCurrentProcessorId();
            // System.Console.WriteLine($"  当前处理器Id：{processorId}");
            // System.Console.WriteLine(RuntimeInformation.RuntimeIdentifier);
            // System.Console.WriteLine(Environment.WorkingSet / 1024.0D / 1024.0D + "MB");

            Process[] p = Process.GetProcesses();//获取进程信息
            Int64 totalMem = 0;
            string info = "";
            foreach (Process pr in p)
            {
                totalMem += pr.WorkingSet64 / 1024;
                info += pr.ProcessName + "内存：-----------" + (pr.WorkingSet64 / 1024).ToString() + "KB\r\n";//得到进程内存
            }

            Console.WriteLine(info);
            Console.WriteLine("总内存totalmem:" + totalMem / 1024 + "M");
            Console.WriteLine("判断是否为Windows Linux OSX");
            Console.WriteLine($"Linux:{RuntimeInformation.IsOSPlatform(OSPlatform.Linux)}");
            Console.WriteLine($"OSX:{RuntimeInformation.IsOSPlatform(OSPlatform.OSX)}");
            Console.WriteLine($"Windows:{RuntimeInformation.IsOSPlatform(OSPlatform.Windows)}");
            Console.WriteLine($"系统架构：{RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"系统名称：{RuntimeInformation.OSDescription}");
            Console.WriteLine($"进程架构：{RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"是否64位操作系统：{Environment.Is64BitOperatingSystem}");
            Console.WriteLine("CPU CORE:" + Environment.ProcessorCount);
            Console.WriteLine("HostName:" + Environment.MachineName);
            Console.WriteLine("Version:" + Environment.OSVersion);

            Console.WriteLine("内存相关的:" + Environment.WorkingSet);
            string[] LogicalDrives = Environment.GetLogicalDrives();
            for (int i = 0; i < LogicalDrives.Length; i++)
            {
                Console.WriteLine("驱动:" + LogicalDrives[i]);
            }
            // Console.ReadLine();

            //创建一个ProcessStartInfo对象 使用系统shell 指定命令和参数 设置标准输出
            // var psi = new ProcessStartInfo("top", " -b -n 1") { RedirectStandardOutput = true };
            var psi = new ProcessStartInfo("top") { RedirectStandardOutput = true };
            //启动
            var proc = Process.Start(psi);

            //   psi = new ProcessStartInfo("", "1") { RedirectStandardOutput = true };
            //启动
            // proc = Process.Start(psi);

            if (proc == null)
            {
                Console.WriteLine("Can not exec.");
            }
            else
            {
                Console.WriteLine("-------------Start read standard output-------cagy-------");
                //开始读取
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (!line.Contains("code")) continue;
                        Console.WriteLine(line);
                    }

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }

                Console.WriteLine($"Total execute time :{(proc.ExitTime - proc.StartTime).TotalMilliseconds} ms");
                Console.WriteLine($"Exited Code ： {proc.ExitCode}");
                Console.WriteLine("---------------Read end-----------cagy-------");
            }
        }

        public static void CpuBlocking()
        {

        }

        public static void MemoryBlocking()
        {

        }
    }
}
