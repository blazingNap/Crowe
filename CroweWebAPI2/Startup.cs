using Autofac;
using Autofac.Integration.WebApi;
//using Autofac.Integration.WebApi;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Crowe.Data.Infrastructure;
using Crowe.Data.Repositories;
using Crowe.Services;
using Microsoft.Owin;
using CroweWebAPI.Controllers;

[assembly: OwinStartup(typeof(CroweWebAPI2.Startup))]

namespace CroweWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Services.Replace(typeof(IAssembliesResolver), new CustomAssembliesResolver());
           // config.Formatters.Add(new CroweFormatter());

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
             );

            // Autofac configuration 
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(ProductController).Assembly);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            //Repositories 
            builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly)
           .Where(t => t.Name.EndsWith("Repository"))
           .AsImplementedInterfaces().InstancePerRequest();
            // Services 
            builder.RegisterAssemblyTypes(typeof(CategoryService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseWebApi(config);
        }
    }
}

