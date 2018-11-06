using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.DTO;
using UserCenter.IServices;

namespace UserCenter.WebApi.Controllers.v1
{
    public class UserController : ApiController
    {
        public IUserService UserService { get; set; }

        /// <summary>
        /// http://localhost:1624/api/v1/User/AddNew?nickName=tom&password=AAAAA&phoneNum=13676001000
        /// 还得在head头中加入参数
        /// AppKey=harryAppKey 【appKey是后台appInfos表中配置的】
        /// sign=(nickName=tom&password=feifei520&password=13676004620harryAppSecret 【qs得根据名称排序后用&组合，计算（qs+AppSecret的值）计算md5值)】
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<long> AddNew(string phoneNum, string nickName, string password)
        {
            return await UserService.AddNewAsync(phoneNum, nickName, password);
        }

        [HttpGet]
        public async Task<bool> UserExists(string phoneNum)
        {
            return await UserService.UserExistsAsync(phoneNum);
        }

        [HttpGet]
        public async Task<bool> CheckLogin(string phoneNum, string password)
        {
            return await UserService.CheckLoginAsync(phoneNum, password);
        }

        /// <summary>
        /// 用Postman测试，报文头中加入appcode
        /// http://localhost:1624/api/v1/User/GetById/1 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDTO> GetById(long id)
        {
            return await UserService.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<UserDTO> GetByPhoneNum(string phoneNum)
        {
            return await UserService.GetByPhoneNumAsync(phoneNum);
        }

    }
}