using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    public class HycLogin
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 密码，采用了MD5编码
        /// </summary>
        public string password { get; set; }
    }
}
