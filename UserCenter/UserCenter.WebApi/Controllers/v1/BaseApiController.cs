using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace UserCenter.WebApi.Controllers.v1
{
    public class BaseApiController: ApiController
    {
        /// <summary>
        /// 重写基类方法[基类不可直接调用]（重写基类方法后需要注意的是，该方法由于返回值是JsonResult,也会被当做API方法，默认都是POST方法，会替换掉没有Route特性的方法，所以临时加了个HttpGet特性。此处还有待完善。项目中碰到的问题，记录一下，欢迎大家拍砖）
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="content">序列化实体</param>
        /// <returns>格式化后的结果</returns>
        [HttpHead, Route("Base/Json")]
        public new JsonResult<T> Json<T>(T content)
        {
            return Json<T>(content,
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings,
                System.Text.Encoding.UTF8);
        }
    }
}