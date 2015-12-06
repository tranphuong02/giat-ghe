using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CL.Framework.Autofac;
using CL.Transverse.Datatables;
using CL.Transverse.ModelBinder;

namespace CL.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyInjectionConfiguration();

            // fetch configuration in first time run
            //Configuration.GetConfiguration();

            // migration database
            Transverse.Migrations.DatabaseMigrations.ApplyDatabaseMigrations();

            // Mapper
            //AutoMapperConfiguration.Configure();

            Mapper.AssertConfigurationIsValid();

            ModelBinders.Binders.Add(typeof(DataTablesParam), new DataTablesModelBinder());
            ModelBinders.Binders.Add(typeof(Decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(Decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(string), new StringModelBinder());
        }

        protected void DependencyInjectionConfiguration()
        {
            //Get all references assemblies
            var coreAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var preferenceAssemblies = coreAssemblies.SelectMany(ca => ca.GetReferencedAssemblies());

            // Load assemblies
            var assemblies = coreAssemblies.Select(ca => ca.GetName())
                .Union(preferenceAssemblies)
                .Distinct()
                .Select((Assembly.Load))
                .ToArray();

            var builder = new ContainerBuilder();
            DependencyRegistrar.Register(builder, assemblies);

            var container = builder.Build();

            DependencyRegistrar.GlobalContainer = container;

            // Register for MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Register for ASP.NET Web API
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}
