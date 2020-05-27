using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace HttpClients
{
    /// <summary>
    /// 将枚举项为中文的枚举类型转换为Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumJsonConvert<T> : JsonConverter where T : struct, IConvertible
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.Parse(objectType, reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
