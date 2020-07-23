namespace StreetLED.Model.Response
{
    /// <summary>
    /// 设备节目信息
    /// </summary>
    public class DeviceProgramsDataModel
    {
        public string uuid { get; set; }
        public string name { get; set; }
        public Schedule schedule { get; set; }
        public string updateTime { get; set; }

    }

    public class Schedule
    {
    }
}
