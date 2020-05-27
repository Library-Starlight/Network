using HttpClients.Services.PartyBuild.Helper;
using HttpClients.Services.PartyBuild.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild
{
    public class PartyBuildPageProvider
    {
        /// <summary>
        /// 智慧党建服务器基地址
        /// </summary>
        private static readonly Uri BaseAddress = new Uri("http://demo.andnext.cn:8632");

        /// <summary>
        /// 获取令牌的路径
        /// </summary>
        private const string GetTokenPath = "oneparkfjzssq/zhshcommunity/community/getToken";
        
        /// <summary>
        /// 社区页面的路径
        /// </summary>
        private const string CommunityPagePath = "oneparkfjzssq/zssqweb#/resident-manage/resident-view";

        /// <summary>
        /// 智慧党建页面的路径
        /// </summary>
        private const string PartyBuildPagePath = "oneparkfjzssq/zssqweb#/partybuild";

        /// <summary>
        /// 查询党组统计的路径
        /// </summary>
        private const string PartySummaryPath = "oneparkfjzssq/zhshcommunity/thirdparty/guide/querySummary";

        /// <summary>
        /// 查询楼层党员数的路径
        /// </summary>
        private const string PartyBuildingPath = "oneparkfjzssq/zhshcommunity/thirdparty/partyBuilding/histogram";

        /// <summary>
        /// 应答消息解码公钥
        /// </summary>
        private const string DecodeKey = "DWYEiW9kqtRs21da";

        /// <summary>
        /// 应答消息解码私钥
        /// </summary>
        private const string DecodeIV = "b5lOJXaGBkLu0yV4";

        #region 公共成员

        /// <summary>
        /// 获取党建页面Uri
        /// </summary>
        /// <returns></returns>
        public async Task<Uri> GePartyPageUri()
        {
            // 获取用户登录令牌
            var token = await GetTokenAsync();

            // 创建访问页面的Uri
            var uri = GetPageUri(PartyBuildPagePath, token.userId, token.token);

            return uri;
        }

        /// <summary>
        /// 获取党员统计
        /// </summary>
        /// <returns></returns>
        public Task<PartySummary> GetPartySummaryAsync()
        {
            return Post<PartySummary>(PartySummaryPath, "{ }");
        }

        /// <summary>
        /// 获取楼层党员统计
        /// </summary>
        /// <returns></returns>
        public Task<List<PartyOfBuilding>> GetPartyOfBuilding()
        {
            return Post<List<PartyOfBuilding>>(PartyBuildingPath, "{ }");
        }

        public async Task<PartyOfBuilding> GetPartyOfBuilding(int floor)
        {
            var datas = await Post<List<PartyOfBuilding>>(PartyBuildingPath, $"{{ \"houseId\":\"{floor.ToString()}\" }}");
            return datas[0];
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T">应答数据体类型</typeparam>
        /// <param name="path">查询路径</param>
        /// <param name="contentStr">请求内容</param>
        /// <returns></returns>
        private async Task<T> Post<T>(string path, string contentStr)
        {
            // 获取用户登录令牌
            var token = await GetTokenAsync();

            // 创建Http客户端
            var client = new HttpClient() { BaseAddress = BaseAddress };

            // 创建Http请求
            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Headers.Add("User-Source", "APP");
            request.Headers.Add("userId", token.userId);
            request.Headers.Add("token", token.token);

            // 创建Http请求内容
            var content = new StringContent(contentStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            var response = await client.SendAsync(request);

            // Http应答结果
            var responseStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseStr);
            var data = ResolveResponseData<T>(responseStr);

            return data;
        }

        /// <summary>
        /// 获取页面访问Uri
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private Uri GetPageUri(string path, string userId, string token)
        {
            return new Uri(BaseAddress, $"{path}?userId={userId}&token={token}");
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <returns></returns>
        private async Task<GetTokenResponse> GetTokenAsync()
        {
            var body = GetRequestBody();

            // 新建Http客户端
            var client = new HttpClient { BaseAddress = BaseAddress };

            // Http请求
            var request = new HttpRequestMessage(HttpMethod.Post, GetTokenPath);

            // Http请求头部
            request.Headers.Add("User-Source", "APP");

            // Http请求内容
            var content = new StringContent(body);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            // 获取Http应答
            var response = await client.SendAsync(request);

            // Http应答内容字符串
            var responseStr = await response.Content.ReadAsStringAsync();

            // 应答实体
            var token = ResolveResponseData<GetTokenResponse>(responseStr);

            return token;
        }

        /// <summary>
        /// 获取Http请求内容
        /// </summary>
        /// <param name="outNonce"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        private string GetRequestBody()
        {
            var time = TimeHelper.DateToUTCTime(DateTime.Now).ToString();
            var nonce = Guid.NewGuid().ToString("N").Substring(0, 16);
            var request = JsonConvert.SerializeObject(new GetTokenRequest
            {
                userId = "83147",
                nonce = "a9028aad1c7a611a",
            });

            var data = GetAes(time, nonce, request);
            var post = JsonConvert.SerializeObject(new AesRequest
            {
                time = time,
                nonce = nonce,
                data = data,
            });

            return post;
        }

        /// <summary>
        /// 解析应答的数据体
        /// </summary>
        /// <param name="responseStr"></param>
        /// <returns></returns>
        private T ResolveResponseData<T>(string responseStr)
        {
            var aesResponse = JsonConvert.DeserializeObject<AesResponse>(responseStr);
            var aesResponseBody = AesEncrypt.DecryptBase64ToOriginal(aesResponse.data, DecodeKey, DecodeIV);
            Console.WriteLine(aesResponseBody);
            var tokenResponse = JsonConvert.DeserializeObject<T>(aesResponseBody);
            return tokenResponse;
        }

        /// <summary>
        /// 获取Aes加密值
        /// </summary>
        /// <param name="time"></param>
        /// <param name="nonce"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private string GetAes(string time, string nonce, string requestBody)
        {
            var original = $"{time}><{requestBody}><{nonce}";
            var data = AesEncrypt.EncryptStringToBase64(original, DecodeKey, nonce);
            return data;
        }

        #endregion
    }
}
