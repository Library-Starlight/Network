using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    /// <summary>
    /// 解析数据成功事件参数
    /// </summary>
    public class ResolvedEventArgs : EventArgs
    {
        public object Data { get; set; }
    }
}
