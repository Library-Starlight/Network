using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace HttpListener
{
    /// <summary>
    /// 将<see cref="DateTime"/>转化为yyyy-MM-dd HH:mm:ss格式的字符串
    /// </summary>
    public class NoDelimiterDateTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyyMMddHHmmss" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }
}
