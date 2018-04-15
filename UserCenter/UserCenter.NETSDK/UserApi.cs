using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NETSDK
{
    public class UserApi
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public UserApi(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }
        public async Task<User> GetByIdAsync(long id)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["id"] = id;
            var result = await client.GetAsync("User/GetById", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<User>(result.Result);
            }
            else
            {
                throw new ApplicationException("GetById失败，状态码"
                    + result.StatusCode + "，响应报文" + result.Result);
            }
        }
    }
}
