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

        public SearchRecordsResponse SearchRecordsResponse { get; }

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

            SearchRecordsResponse = new SearchRecordsResponse
            {
                msg = string.Empty,
                code = string.Empty,
                data = new List<RecordsData>
                {
                    new RecordsData(),
                    new RecordsData(),
                    new RecordsData(),
                    new RecordsData(),
                    new RecordsData(),
                    new RecordsData(),
                },
            };

            SearchRemainSpaceResponse = new SearchRemainSpaceResponse
            {
                msg = string.Empty,
                code = string.Empty,
                data = new List<RemainSpaceData>
                {
                    new RemainSpaceData(),
                    new RemainSpaceData(),
                    new RemainSpaceData(),
                    new RemainSpaceData(),
                    new RemainSpaceData(),
                },
            };
        }

        #endregion
    }
}
