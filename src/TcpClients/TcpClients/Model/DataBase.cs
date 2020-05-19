using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    public abstract class DataBase
    {
        /// <summary>
        /// 发送目标Id
        /// </summary>
        public ushort TargetId { get; set; }

        /// <summary>
        /// 发送方Id
        /// </summary>
        public ushort SenderId { get; set; }

        /// <summary>
        /// 命令字
        /// </summary>
        public CommandType CommandType { get; set; }
    }
}
