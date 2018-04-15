using RuPeng.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NETSDK
{
     class SDKClient
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;//(openapi的服务器访问地址约定是 http://127.0.0.1:8888/api/v1/)
        public SDKClient(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }
        /// <summary>
        /// 请求服务器对于接口
        /// </summary>
        /// <param name="url">请求的url，例如：User\GetById</param>
        /// <param name="queryStringData">参数，id=1 键值对</param>
        /// <returns></returns>
        public async Task<SDKResult> GetAsync(string url,IDictionary<string,object> queryStringData)
        {
            if (queryStringData==null)
            {
                throw new ArgumentException("queryStringData不能为空");
            }
            var qsItems = queryStringData.OrderBy(kv => kv.Key)
              .Select(kv => kv.Key + "=" + kv.Value);
            var queryString = string.Join("&", qsItems);
            string sign = MD5Helper.ComputeMd5(queryString + appSecret);
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Add("AppKey", appKey);
                hc.DefaultRequestHeaders.Add("Sign", sign);
                var resp = await hc.GetAsync(serverRoot + url + "?" + queryString);
                SDKResult sdkResult = new SDKResult();
                sdkResult.Result = await resp.Content.ReadAsStringAsync();
                sdkResult.StatusCode = resp.StatusCode;
                return sdkResult;
            }

        }
    }
}
