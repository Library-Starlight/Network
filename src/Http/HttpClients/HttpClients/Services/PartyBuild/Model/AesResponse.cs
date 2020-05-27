using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild.Model
{
    public class AesResponse
    {
        public bool status { get; set; }
        public string msg { get; set; }
        public string code { get; set; }
        public string data { get; set; }
    }
}
