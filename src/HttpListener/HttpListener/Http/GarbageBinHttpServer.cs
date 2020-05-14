using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 垃圾桶数据Http接收服务器
    /// </summary>
    public class GarbageBinHttpServer
    {
        #region 私有字段

        private readonly IPEndPoint _endPoint;

        #endregion

        #region 公共属性

        /// <summary>
        /// 垃圾桶列表
        /// </summary>
        public IDictionary<string, GarbageBin> GarbageBinList { get; set; } = new Dictionary<string, GarbageBin>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="endPoint">服务器监听端点</param>
        public GarbageBinHttpServer(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 启动
        /// </summary>
        public async void StartAsync()
        {
            var listener = new System.Net.HttpListener();
            listener.Prefixes.Add($"http://{_endPoint.ToString()}/GarbageBin/Data/");

            // 启动服务器
            try
            {
                listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"智慧垃圾桶，启动服务器失败，{ex.ToString()}");
                return;
            }

            // 消息接收
            while (true)
            {
                try
                {
                    var context = await listener.GetContextAsync();
                    var result = await ReceiveMessage(context);
                    await SendResponse(context, result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"智慧垃圾桶，获取请求失败，{ex.ToString()}");
                }
            }
        }

        /// <summary>
        /// 获取垃圾桶状态
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="propertyName">状态名称</param>
        /// <param name="value">状态值</param>
        /// <returns>获取是否成功</returns>
        public bool TryGetGarbageBinStatus(string deviceId, string propertyName, out object value)
        {
            if (!GarbageBinList.ContainsKey(deviceId))
            {
                value = default;
                return false;
            }

            var bin = GarbageBinList[deviceId];

            // 通过反射获取垃圾桶状态值
            var prop = bin.GetType().GetProperty(propertyName);
            if (prop == null)
            {
                value = default;
                return false;
            }

            value = prop.GetValue(bin);
            return true;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取指令名称
        /// </summary>
        /// <param name="json">json原始数据</param>
        /// <returns></returns>
        private bool TryGetCmd(string json, out RequestCmd cmd)
        {
            var jObj = JObject.Parse(json);

            var cmdNode = jObj["cmd"];

            // 未找到节点
            if (cmdNode == null)
            {
                cmd = default;
                return false;
            }

            return Enum.TryParse<RequestCmd>(cmdNode.ToString(), out cmd);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        private async Task<bool> ReceiveMessage(HttpListenerContext context)
        {
            try
            {
                using (var stream = context.Request.InputStream)
                using (var sr = new StreamReader(stream))
                {
                    var json = await sr.ReadToEndAsync();

                    // 打印接收日志
                    Console.WriteLine($"智慧垃圾桶，接收数据：{json}");

                    // 获取请求指令名称
                    if (!TryGetCmd(json, out var cmd))
                        return false;

                    // 更新垃圾桶状态
                    switch (cmd)
                    {
                        case RequestCmd.sendGarbagebinStatuInfo:
                            var status = JsonConvert.DeserializeObject<GarbageBinStatuInfo>(json);
                            UpdateGarbageBin(status);
                            break;
                        case RequestCmd.sendPulse:
                            var pulse = JsonConvert.DeserializeObject<Pulse>(json);
                            UpdateGarbageBin(pulse);
                            break;
                        case RequestCmd.sendGarbagebinOpenStatuUp:
                            var openStatu = JsonConvert.DeserializeObject<GarbageBinOpenStatuUp>(json);
                            UpdateGarbageBin(openStatu);
                            break;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"智慧垃圾桶，接收数据失败：{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 发送应答
        /// </summary>
        /// <param name="context"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        private async Task SendResponse(HttpListenerContext context, bool success)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            using (var stream = context.Response.OutputStream)
            using (var sw = new StreamWriter(stream))
            {
                var result = new PostResult()
                {
                    code = success ? 100 : 101,
                    body = new Body()
                    {
                        msg = success ? "成功" : "失败",
                    },
                };

                var json = JsonConvert.SerializeObject(result);
                await sw.WriteAsync(json);
            }
        }

        /// <summary>
        /// 更新垃圾桶
        /// </summary>
        /// <param name="status">垃圾桶满溢状态</param>
        private void UpdateGarbageBin(GarbageBinStatuInfo status)
        {
            var bin = GetGarbageBin(status.deviceID);
            bin.UpdateTime = status.time;
            bin.Rssi = status.data.rssi;
            bin.Full = status.data.full != 0;
            bin.Distance = status.data.distance;
            bin.Battary = status.data.battary;
        }

        /// <summary>
        /// 更新垃圾桶
        /// </summary>
        /// <param name="pulse">心跳信息</param>
        private void UpdateGarbageBin(Pulse pulse)
        {
            var bin = GetGarbageBin(pulse.deviceID);
            bin.UpdateTime = pulse.time;
            bin.Rssi = pulse.data.rssi;
            bin.Angle = pulse.data.angle;
            bin.Distance = pulse.data.distance;
            bin.Battary = pulse.data.battary;
        }

        /// <summary>
        /// 更新垃圾桶
        /// </summary>
        /// <param name="openStatu">垃圾桶上盖状态</param>
        private void UpdateGarbageBin(GarbageBinOpenStatuUp openStatu)
        {
            var bin = GetGarbageBin(openStatu.deviceID);
            bin.UpdateTime = openStatu.time;
            bin.Rssi = openStatu.data.rssi;
            bin.Angle = openStatu.data.angle;
            bin.OpenState = openStatu.data.state != 0;
            bin.Battary = openStatu.data.battary;
        }

        /// <summary>
        /// 获取垃圾桶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private GarbageBin GetGarbageBin(string id)
        {
            if (GarbageBinList.ContainsKey(id))
                return GarbageBinList[id];

            var garbageBin = new GarbageBin();
            GarbageBinList[id] = garbageBin;
            return garbageBin;
        }

        #endregion
    }
}
