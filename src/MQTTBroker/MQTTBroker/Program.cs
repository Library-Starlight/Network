using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mqtt;
using System.Net.Mqtt.Sdk;
using System.Text;
using System.Threading.Tasks;

namespace MQTTBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mqttConfig = new MqttConfiguration
            //{
            //    BufferSize = 128 * 1024,
            //    Port = 10050,
            //    KeepAliveSecs = 10,
            //    WaitTimeoutSecs = 2,
            //    MaximumQualityOfService = MqttQualityOfService.AtLeastOnce,
            //    AllowWildcardsInTopicFilters = true,
            //};

            var server = MqttServer.Create(10050);
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.MessageUndelivered += Server_MessageUndelivered;
            server.Stopped += Server_Stopped;
            server.Start();

            Console.WriteLine($"Server running on port 10050...");
            Console.ReadLine();
        }

        private static void Server_Stopped(object sender, MqttEndpointDisconnected e)
        {
            Console.WriteLine(JsonConvert.SerializeObject(e));
        }

        private static void Server_MessageUndelivered(object sender, MqttUndeliveredMessage e)
        {
            Console.WriteLine(JsonConvert.SerializeObject(e));
        }

        private static void Server_ClientDisconnected(object sender, string e)
        {
            Console.WriteLine(e);
        }

        private static void Server_ClientConnected(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}
