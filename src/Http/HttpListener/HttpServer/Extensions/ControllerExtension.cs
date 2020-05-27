using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtension
    {
        /// <summary>
        /// 记录请求头部日志
        /// </summary>
        /// <param name="controller"></param>
        public static void LogHeaders(this Controller controller)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}{Environment.NewLine}Request Headers:");
            foreach (var header in controller.ControllerContext.HttpContext.Request.Headers)
                Console.WriteLine($"{header.Key}:{header.Value}");
            Console.WriteLine();
        }

        /// <summary>
        /// 记录请求内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="body"></param>
        public static void LogBody(this Controller controller, object body)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}{Environment.NewLine}Request Body:");
            var content = JsonConvert.SerializeObject(body);
            Console.WriteLine(content);
            Console.WriteLine();
        }
    }
}
