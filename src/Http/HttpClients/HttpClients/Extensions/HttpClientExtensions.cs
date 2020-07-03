using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">应答类型</typeparam>
        /// <param name="client">Http客户端</param>
        /// <param name="content">请求内容</param>
        /// <returns></returns>
        public static async Task<WebRequestResult<T>> PostAsJsonAsync<T>(this HttpClient client, string uri, object content)
        {
            var result = new WebRequestResult<T>()
            {
                Successful = false,
                ServerResponse = default(T),
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = null,
            };

            var contentStr = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(contentStr, Encoding.UTF8);

            try
            {
                var responseMessage = await client.PostAsync(uri, httpContent);

                result.StatusCode = responseMessage.StatusCode;
                result.Successful = responseMessage.IsSuccessStatusCode;

                // 请求成功时，解析应答内容
                if (result.Successful)
                {
                    var responseStr = await responseMessage.Content.ReadAsStringAsync();
                    result.ServerResponse = JsonConvert.DeserializeObject<T>(responseStr);
                }
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
