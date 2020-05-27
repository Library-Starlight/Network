using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpShared.Hikvision
{
    public abstract class HikResponse<T>
    {
        public string code { get; set; }

        public string msg { get; set; }

        public abstract List<T> data { get; set; }
    }
}
