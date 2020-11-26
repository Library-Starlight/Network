using System;
using System.IO.BACnet;
using System.Net;

// BACnet客户端
//var client = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC1, true, localEndpointIp: "192.168.229.1"));
//var client = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC0, true, localEndpointIp: "192.168.3.109"));
var client = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC0, false));
client.Start();

// 设备列表
client.WhoIs();
client.OnIam += IamCallback;
client.OnWhoIs += WhoIsCallback;
Console.WriteLine("Start success.");

// 获取设备地址
var ip = IPAddress.Parse("192.168.229.129");
var port = 47808;
//var ip = IPAddress.Parse("192.168.1.154");
//var port = 47809;
var endpoint = new IPEndPoint(ip, port);
BacnetIpUdpProtocolTransport.Convert(endpoint, out var address);

// 读取属性
var objId = BacnetObjectId.Parse("OBJECT_ANALOG_INPUT:5");
var propId = BacnetPropertyIds.PROP_PRESENT_VALUE;
var values = await client.ReadPropertyAsync(address, objId, propId);
Console.WriteLine(values.Count);
Console.ReadLine();

void WhoIsCallback(BacnetClient sender, BacnetAddress adr, int lowLimit, int highLimit)
{
    Console.WriteLine($"addr: {adr}, lowlimit: {lowLimit}, highlimit: {highLimit}");
}

void IamCallback(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxAPDU, BacnetSegmentations segmentation, ushort vendorId)
{
    Console.WriteLine($"addr: {adr}, deviceId: {deviceId}");
}
