using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild.Model
{
    public class PartyOfBuilding
    {
        /// <summary>
        /// 楼层Id
        /// </summary>
        [JsonProperty("houseId")]
        public int HouseId { get; set; }

        /// <summary>
        /// 楼层号码
        /// </summary>
        [JsonProperty("houseNum")]
        public string HouseNum { get; set; }

        /// <summary>
        /// 党员数
        /// </summary>
        [JsonProperty("partyMembers")]
        public int PartyMembers { get; set; }
    }
}
