using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Helper
{
    /// <summary>
    /// 在控制台打印给定对象的元数据描述。
    /// 该方法用于调试和开发期间，对对象的运行时状态进行观察。
    /// </summary>
    public class ObjectHelper
    {
        public static void GetPropertyInfo(object obj)
        {
            var type = obj.GetType();
            // 缩进字符数
            var indent = 20;
            
            var props = type.GetProperties();
            Console.WriteLine($"{"Name".PadLeft(indent)} | Value");
            foreach (var prop in props)
                Console.WriteLine($"{prop.Name.PadLeft(indent)} | {prop.GetValue(obj)}");
        }
    }
}
