using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sockets
{
    /// <summary>
    /// The EF magic unicorn.
    /// </summary>
    public class Unicorn
    {
        const int CHAR = 0x100;
        const int SEQ = 0x10000;
        const int CHARMASK = CHAR - 1;

        public override string ToString() =>
            new string(new[]
            {
                0x02015, 0x15F2F, 0x0005C, 0x05F02, 0x00001, 0x0200F, 0x02D03,
                0x03D02, 0x0002F, 0x02004, 0x05C02, 0x00001, 0x02009, 0x05F03,
                0x02002, 0x05F03, 0x02003, 0x17C2E, 0x02004, 0x15C7C, 0x15C01,
                0x02008, 0x17C20, 0x05F02, 0x07C02, 0x00020, 0x05F02, 0x0007C,
                0x02002, 0x0007C, 0x02002, 0x00029, 0x02003, 0x05C03, 0x00001,
                0x02008, 0x17C20, 0x15F7C, 0x1207C, 0x1205F, 0x0007C, 0x02003,
                0x15C5F, 0x12F20, 0x0007C, 0x02002, 0x02F02, 0x0007C, 0x05C02,
                0x00001, 0x02008, 0x0007C, 0x05F03, 0x07C02, 0x15F7C, 0x02007,
                0x0002F, 0x02003, 0x05C03, 0x0002F, 0x05C02
            }.SelectMany(
                    i => i < CHAR ?
                        new[] { (char)i } :
                        i < SEQ ?
                            new string((char)(i >> 8), i & CHARMASK).ToCharArray() :
                            new[] { (char)((i - SEQ) >> 8), (char)((i - SEQ) & CHARMASK) }
                            ).ToArray())
            .Replace(new string(new[] { (char)0x01 }), Environment.NewLine);
    }
}
