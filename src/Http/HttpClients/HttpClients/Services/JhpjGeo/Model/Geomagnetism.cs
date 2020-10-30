using System;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.Model
{
    public class Geomagnetism
    {
        /// <summary>
        /// 地磁Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 是否有车停放（空：未知，0：无车，1：有车
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline => (DateTime.Now - ReceiveTime).TotalDays <= 1.0D;

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="status"></param>
        public void Update(Status status)
        {
            Id = status.serial;
            State = status.state;
            UploadTime = DateTime.Parse(status.event_time);
            ReceiveTime = DateTime.Now;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="info"></param>
        public void Update(Info info)
        {
            Id = info.serial;
            UploadTime = DateTime.Parse(info.event_time);
            ReceiveTime = DateTime.Now;
        }
    }
}
