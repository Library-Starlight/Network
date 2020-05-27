using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild.Model
{
    public class PartySummary
    {
        /// <summary>
        /// 党组总数
        /// </summary>
        [JsonProperty("termSum")]
        public int TermSum { get; set; }

        /// <summary>
        /// 党支部总数
        /// </summary>
        [JsonProperty("branchSum")]
        public int BranchSum { get; set; }

        /// <summary>
        /// 党员总数
        /// </summary>
        [JsonProperty("partySum")]
        public int PartySum { get; set; }

        /// <summary>
        /// 党员活动数
        /// </summary>
        [JsonProperty("activitySum")]
        public int ActivitySum { get; set; }
    }
}
