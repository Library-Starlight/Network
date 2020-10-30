using Jhpj.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.Data
{
    public class GeomagnetismProvider
    {
        #region 单例

        private static readonly GeomagnetismProvider _instance;

        public static GeomagnetismProvider Instance
        {
            get => _instance;
        }

        static GeomagnetismProvider()
        {
            _instance = new GeomagnetismProvider();
        }

        #endregion

        #region 私有字段

        private readonly ConcurrentDictionary<string, Geomagnetism> _geomagnetisms = new ConcurrentDictionary<string, Geomagnetism>();

        #endregion

        #region 添加地磁

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data">车位状态</param>
        public void Add(Status data)
        {
            Geomagnetism geo;
            if (!_geomagnetisms.ContainsKey(data.serial) || !_geomagnetisms.TryGetValue(data.serial, out geo))
            {
                geo = new Geomagnetism();
                _geomagnetisms.TryAdd(data.serial, geo);
            }

            geo.Update(data);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data">工作状态</param>
        public void Add(Info data)
        {
            Geomagnetism geo;
            if (!_geomagnetisms.ContainsKey(data.serial) || !_geomagnetisms.TryGetValue(data.serial, out geo))
            {
                geo = new Geomagnetism();
                _geomagnetisms.TryAdd(data.serial, geo);
            }

            geo.Update(data);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">地磁id</param>
        /// <returns></returns>
        public Geomagnetism Get(string key)
        {
            Geomagnetism geo;
            _geomagnetisms.TryGetValue(key, out geo);
            return geo;
        }

        #endregion
    }
}
