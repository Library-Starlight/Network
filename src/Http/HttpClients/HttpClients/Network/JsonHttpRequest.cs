using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class JsonHttpRequest
    {
        /// <summary>
        /// 发送Post请求，对结果进行Json解析
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">应答类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="body">请求内容</param>
        /// <param name="param">Query字符串参数</param>
        /// <param name="headers">请求头部</param>
        /// <returns>应答内容</returns>
        public static async Task<TResponse> PostFromJsonAsync<TResponse>(string url, object body, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            var bodyJson = JsonConvert.SerializeObject(body);
            var result = await HttpRequest.PostAsync(url, bodyJson, param, headers);
            var response = JsonConvert.DeserializeObject<TResponse>(result);
            return response;
        }

        /// <summary>
        /// 发送Get请求，对结果进行Json解析
        /// </summary>
        /// <typeparam name="TResponse">应答类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="param">Query字符串参数</param>
        /// <param name="headers">请求头部</param>
        /// <returns>应答内容</returns>
        public static async Task<TResponse> GetFromJsonAsync<TResponse>(string url, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            var result = await HttpRequest.GetAsync(url, param: param, headers: headers);
            var response = JsonConvert.DeserializeObject<TResponse>(result);
            return response;
        }

        /// <summary>
        /// 发送Post请求，对结果进行Json解析
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<TResponse> PostFormDataFromJsonAsync<TResponse>(string url, IDictionary<string, string> param)
        {
            var client = new HttpClient();

            var data = param.ToQueryString();
            HttpContent content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var responseMessage = await client.PostAsync(url, content);

            var responseStr = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(responseStr);
            return response;
        }
    }
}
