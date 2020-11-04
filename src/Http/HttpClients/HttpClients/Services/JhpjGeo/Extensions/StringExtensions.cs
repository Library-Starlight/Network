namespace System
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.TrimStart('0', 'x', 'X');
            value = value.Replace(" ", string.Empty);
            var data = new byte[value.Length / 2];
            for (int i = 0; i < value.Length - 1; i += 2)
            {
                data[i / 2] = byte.Parse(value.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return data;
        }
    }
}
