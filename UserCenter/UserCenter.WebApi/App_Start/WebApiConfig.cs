using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using UserCenter.WebApi.Filters;

namespace UserCenter.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            ////配置返回的时间类型数据格式首字母小写
            ReturnJsonSerializerSettings();

            config.Services.Replace(typeof(IHttpControllerSelector),
                new VersionControllerSelector(config));

            //一个对象必须是IOC容器创建出来的，IOC容器才会自动帮我们注入
            UCAuthorizationFilter authorFilter = (UCAuthorizationFilter)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(UCAuthorizationFilter));
            config.Filters.Add(authorFilter);


        }
        /// <summary>
        /// 配置返回的时间类型数据格式首字母小写
        /// </summary>
        private static void ReturnJsonSerializerSettings()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            json.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//首字母小写
        }
    }
}
