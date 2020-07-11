using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.AwesomeUI
{
    public class LoginDesignModel : LoginViewModel
    {
        /// <summary>
        /// 线程同步锁
        /// </summary>
        private readonly static object _objLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static LoginDesignModel _instance;

        public static LoginDesignModel Instance
        {
            get
            {
                if (_instance == null)
                    lock (_objLock)
                        if (_instance == null)
                            _instance = new LoginDesignModel();
                return _instance;
            }
        }
    }
}
