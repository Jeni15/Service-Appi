using Autofac;
using Autofac.Integration.Web;
using Base.Data.Infrastructure;
using Base.Data.Xml;
using Base.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Base.Web
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider => _containerProvider;

        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetAutofacContainer();
            SqlManager.Create();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

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

            _containerProvider = new ContainerProvider(builder.Build());

        }
    }
}