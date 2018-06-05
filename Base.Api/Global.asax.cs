using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.WebApi;
using Base.Data.Infrastructure;
using Base.Data.Xml;
using Base.Service.Infrastructure;

namespace Base.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SetAutofacContainer();
            SqlManager.Create();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;


            builder.RegisterType<AdoNetUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();


            builder.RegisterType<AdoNetDbFactory>()
                .As<IAdoNetDbFactory>()
                .InstancePerRequest();


            builder.RegisterType<AdoNetDbConnectionFactory>()
                .As<IAdoNetDbConnectionFactory>()
                .InstancePerRequest();


            builder.RegisterAssemblyTypes(Assembly.Load("Base.Data"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();


            builder.RegisterAssemblyTypes(Assembly.Load("Base.Service"))
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerRequest();

            builder.RegisterGeneric(typeof(AdoNetRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerDependency();


            builder.RegisterGeneric(typeof(EntityService<>))
            .As(typeof(IEntityService<>))
            .InstancePerDependency();


            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);



        }
    }
}
