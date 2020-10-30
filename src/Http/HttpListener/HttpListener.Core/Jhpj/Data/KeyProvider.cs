using Jhpj.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.Data
{
    public class KeyProvider
    {
        #region 单例

        private static readonly object _objLock = new object();

        private static readonly KeyProvider _instance;

        public static KeyProvider Instance
        {
            get => _instance;
        }

        private KeyProvider() { }

        static KeyProvider()
        {
            _instance = new KeyProvider();
        }

        #endregion

        #region 私有字段

        private ConcurrentDictionary<string, KeyCache> _keyCaches = new ConcurrentDictionary<string, KeyCache>();

        #endregion

        #region 公共方法

        /// <summary>
        /// 添加Key信息
        /// </summary>
        public void AddKey(string appId, string privateKey, string publicKey, string encryptKey)
        {
            if (_keyCaches.ContainsKey(appId))
                return;

            var keyCache = new KeyCache
            {
                AppId = appId,
                PrivateKey = privateKey,
                PublicKey = publicKey,
                EncryptKey = encryptKey,
            };
            _keyCaches.TryAdd(appId, keyCache);
        }

        /// <summary>
        /// 获取Key信息
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public KeyCache GetKey(string appId)
        {
            _keyCaches.TryGetValue(appId, out var keyCache);
            return keyCache;
        }

        #endregion
    }
}
