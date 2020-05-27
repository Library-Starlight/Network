using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpShared.Hikvision;
using HttpShared.Hikvision.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpServer.Controllers
{
    [Route("/api/pms/v1/park/remainSpaceNum")]
    [ApiController]
    public class RemainSpaceNumController : Controller
    {
        /// <summary>
        /// 日志类
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// 接口设计时数据
        /// </summary>
        private HikResponseDesignData _designData;

        public RemainSpaceNumController(ILogger<RemainSpaceNumController> logger, HikResponseDesignData designData)
        {
            _logger = logger;
            _designData = designData;
        }

        [HttpPost]
        public SearchRemainSpaceResponse Post(SearchRemainSpace request)
        {
            this.LogHeaders();
            this.LogBody(request);

            return _designData.SearchRemainSpaceResponse;
        }
    }

    [Route("/api/pms/v1/crossRecords/page")]
    [ApiController]
    public class CrossRecordController : Controller
    {
        /// <summary>
        /// 接口设计时数据
        /// </summary>
        private HikResponseDesignData _designData;

        public CrossRecordController(HikResponseDesignData designData)
        {
            _designData = designData;
        }

        [HttpPost]
        public SearchRecordsResponse Post(SearchRecords request)
        {
            this.LogHeaders();
            this.LogBody(request);

            return _designData.SearchRecordsResponse;
        }
    }

    [Route("/api/pms/v1/image")]
    [ApiController]
    public class ImageController : Controller
    {
        /// <summary>
        /// 接口设计时数据
        /// </summary>
        private HikResponseDesignData _designData;

        public ImageController(HikResponseDesignData designData)
        {
            _designData = designData;
        }

        [HttpPost]
        public SearchImageResponse Post(SearchImage request)
        {
            this.LogHeaders();
            this.LogBody(request);

            return _designData.SearchImageResponse;
        }
    }
}