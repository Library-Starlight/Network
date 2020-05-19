using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace HttpListener.Http
{
    public class Listener
    {
        public async Task Listen(ushort port)
        {
            try
            {
                using (System.Net.HttpListener listener = new System.Net.HttpListener())
                {
                    listener.Prefixes.Add($"http://localhost:{port.ToString()}/Device/Data/");
                    listener.Start();
                    Console.WriteLine("开始监听");
                    while (true)
                    {
                        try
                        {
                            HttpListenerContext context = await listener.GetContextAsync();//阻塞
                            HttpListenerRequest request = context.Request;
                            string postData = new StreamReader(request.InputStream).ReadToEnd();
                            Console.WriteLine("收到请求：" + postData);
                            HttpListenerResponse response = context.Response;//响应
                            string responseBody = "OK";
                            response.ContentLength64 = System.Text.Encoding.UTF8.GetByteCount(responseBody);
                            response.StatusCode = 200;

                            //输出响应内容
                            Stream output = response.OutputStream;
                            using (StreamWriter sw = new StreamWriter(output))
                            {
                                sw.Write(responseBody);
                            }
                            Console.WriteLine("响应结束");
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("程序异常，请重新打开程序：" + err.Message);
            }
        }
    }
}
