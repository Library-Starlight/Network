using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net
{
    public class ClientReceivedDataEventArgs: EventArgs
    {
        #region 私有字段



        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        public ClientReceivedDataEventArgs(AsyncTcpClient client, byte[] data)
        {

        }

        #endregion
    }
}
