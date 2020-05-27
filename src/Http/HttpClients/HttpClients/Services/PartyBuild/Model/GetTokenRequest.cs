using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild.Model
{
    public class GetTokenRequest
    {
        public string userId { get; set; }
        public string nonce { get; set; }
    }
}
