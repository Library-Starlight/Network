using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    public enum ChannelType : byte
    {
        系统通道 = 0x00,
        大气温度通道 = 0x01,
		大气湿度通道 = 0x02,
        大气压力通道 = 0x03,
        风速通道 = 0x82,
        风向通道 = 0x83,
        降雨通道 = 0x06,
        总辐射通道 = 0x0A,
        PM25通道 = 0x70,
        PM10通道 = 0x69,
        噪声通道 = 0x71,
        GPS通道 = 0x15,
	}
}
