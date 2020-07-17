using Newtonsoft.Json;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        public static async Task<WebRequestResult<TResult>> GetAsJsonAsync<TResult>(this HttpClient client, string uri)
        {
            var result = BuildResult<TResult>();

            try
            {
                // 发起Http Get请求
                var responseMessage = await client.GetAsync(uri);

                await UpdateResultAsync(result, responseMessage);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Http Post请求
        /// </summary>
        /// <typeparam name="T">应答类型</typeparam>
        /// <param name="client">Http客户端</param>
        /// <param name="content">请求内容</param>
        /// <returns></returns>
        public static async Task<WebRequestResult<TResult>> PostAsJsonAsync<TResult, TValue>(this HttpClient client, string uri, TValue value)
        {
            var result = BuildResult<TResult>();

            var contentStr = JsonConvert.SerializeObject(value);
            var httpContent = new StringContent(contentStr, Encoding.UTF8);

            try
            {
                // 发起Http Post请求
                var responseMessage = await client.PostAsync(uri, httpContent);

                await UpdateResultAsync(result, responseMessage);
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #region 帮助方法

        /// <summary>
        /// 构建Http应答结果
        /// </summary>
        /// <typeparam name="TResult">结果内容</typeparam>
        /// <returns></returns>
        private static WebRequestResult<TResult> BuildResult<TResult>()
        {
            var result = new WebRequestResult<TResult>()
            {
                Successful = false,
                ServerResponse = default(TResult),
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = null,
            };

            return result;
        }

        private static async Task UpdateResultAsync<TResult>(WebRequestResult<TResult> result, HttpResponseMessage responseMessage)
        {
            result.StatusCode = responseMessage.StatusCode;
            result.Successful = responseMessage.IsSuccessStatusCode;

            // 请求成功时，解析应答内容
            if (result.Successful)
            {
                var responseStr = await responseMessage.Content.ReadAsStringAsync();
                result.ServerResponse = JsonConvert.DeserializeObject<TResult>(responseStr);
            }
        }

        #endregion
    }
}
