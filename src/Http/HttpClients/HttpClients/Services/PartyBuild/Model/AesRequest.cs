using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild.Model
{
    public class AesRequest
    {
        public string time { get; set; }
        public string nonce { get; set; }
        public string data { get; set; }
    }
}
