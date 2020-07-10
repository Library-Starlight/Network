using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Model.Enums
{
    public enum ProtocolType : uint
    {
        CMCC30 = 0x80000001,

        JTB808V2013 = 0x08082013,
        JTB808V2019 = 0x08082019,
        JTB809V2013 = 0x08092013,
        JTB809V2019 = 0x08092019,
    }
}
