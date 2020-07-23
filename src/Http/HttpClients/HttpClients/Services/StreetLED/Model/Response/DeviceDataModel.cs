namespace StreetLED.Model.Response
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceDataModel
    {
        public string sn { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public bool isOnline { get; set; }
        public float lng { get; set; }
        public float lat { get; set; }
        public string heartbeatTime { get; set; }
        public int state { get; set; }
    }
}
