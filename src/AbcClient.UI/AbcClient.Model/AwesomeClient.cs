using AbcClient.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Model
{
    public class AwesomeClient
    {
        /// <summary>
        /// Id标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文件夹Id
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务器Ip地址
        /// </summary>
        public string ServerIp { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public ushort ServerPort { get; set; }

        /// <summary>
        /// 客户端所有者
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 客户端所有者电话
        /// </summary>
        public ulong Phone { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        public ProtocolType ProtocolType { get; set; }
    }
}
