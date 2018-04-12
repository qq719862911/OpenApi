using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UserCenter.IServices;

namespace UserCenter.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            InitAutoFac();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          
        }
        private void InitAutoFac() {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            //register api controller using assembly scanning
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(configuration);
            //一个对象必须是IOC容器创建出来的，IOC容器才会自动帮我们注入

            var services = Assembly.Load("UserCenter.Services");
            builder.RegisterAssemblyTypes(services)
                .Where(type => !type.IsAbstract && typeof(IServiceTag).IsAssignableFrom(type))
                .AsImplementedInterfaces().SingleInstance().PropertiesAutowired();

            var container = builder.Build();
            // Set the WebApi dependency resolver.  
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}
