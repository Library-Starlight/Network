using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpShared.Hikvision.Design
{
    public class HikResponseDesignData
    {
        #region 公共属性

        public SearchImageResponse SearchImageResponse { get; }

        public SearchRecordsResponse SearchRecordsResponse
        {
            get
            {
                var response = new SearchRecordsResponse
                {
                    msg = string.Empty,
                    code = string.Empty,
                    data = new List<RecordsData>
                {
                    new RecordsData
                    {
                        pageNo = 1,
                        list = new List<RecordItem>(),
                    },
                },
                };

                var now = DateTime.Now;
                // 生成24小时停车记录
                for (int i = 0; i < 24; i++)
                {
                    var random = new Random(Guid.NewGuid().GetHashCode());
                    var flow = random.Next(0, 150);
                    for (int j = 0; j < flow; j++)
                    {
                        var minute = random.Next(0, 60);
                        var second = random.Next(0, 60);
                        var time = new DateTime(now.Year, now.Month, now.Day, i, minute, second);
                        response.data[0].list.Add(new RecordItem
                        {
                            plateNo = "粤B 9CI6R",
                            cardNo = "TC - 13550957796",
                            crossTime = time,
                            vehicleOut = 1,
                        });
                    }
                }
                response.data[0].pageSize = response.data[0].total = response.data[0].list.Count;
                return response;
            }
        }

        public SearchRemainSpaceResponse SearchRemainSpaceResponse { get; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public HikResponseDesignData()
        {
            SearchImageResponse = new SearchImageResponse
            {
                msg = "",
                code = "",
            };

            SearchRemainSpaceResponse = new SearchRemainSpaceResponse
            {
                msg = string.Empty,
                code = string.Empty,
                data = new List<RemainSpaceData>
                {
                    new RemainSpaceData
                    {
                        totalPlace = 320,
                        leftPlace = 55,
                    }
                },
            };
        }

        #endregion
    }
}
