using System;
using System.Net;

namespace Sockets.Extension
{
    public static class IPAddressExtensions
    {
        public static uint HostToNetworkOrder(this uint v)
            => (v & 0xFFFF0000) >> 16 | (v & 0x0000FFFF) << 16;

        public static ushort HostToNetworkOrder(this ushort v)
            => (ushort)(((int)v & 0xFF00) >> 8 | ((int)v & 0x00FF) << 8);

        public static ulong HostToNetworkOrder(this ulong v)
            => (ulong)IPAddress.HostToNetworkOrder((long)v);
     }
}
