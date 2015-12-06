using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CL.Framework.Autofac.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CL.Framework.Autofac
{
    /// <summary>
    /// Class to register dependencies with the container builder
    /// </summary>
    public static class DependencyRegistrar
    {
        public static IContainer GlobalContainer { get; set; }

        public static T Resolve<T>()
        {
            return GlobalContainer.Resolve<T>();
        }

        /// <summary>
        /// Build  the dependency container
        /// </summary>
        /// <param name="builder">Autofac container builder</param>
        /// <param name="assemblies">Application assemblies</param>
        public static void Register(ContainerBuilder builder, IEnumerable<Assembly> assemblies)
        {
            var types = assemblies.SelectMany(x =>
            {
                try
                {
                    return x.GetExportedTypes();
                }
                catch
                {
                    return new Type[] { };
                }
            })
                .Where(x => !x.IsAbstract && !x.IsInterface).ToList();

            // register autofac modules
            RegisterModules(types.Where(type => typeof(IModule).IsAssignableFrom(type)), builder);

#if DEBUG
            bool checkContextRegisted =
                (types.Where(type => typeof(IDependency)
                .IsAssignableFrom(type))
                .FirstOrDefault(i => i.Name.Contains("Context")) != null);

            System.Diagnostics.Debug.WriteLine("Context is registered: " + checkContextRegisted, checkContextRegisted ? "INFO" : "ERROR");
#endif

            // register dependencies
            RegisterDependencies(types.Where(type => typeof(IDependency).IsAssignableFrom(type)), builder);

            // register controllers & API controllers
            builder.RegisterControllers((Assembly[])assemblies);
            builder.RegisterApiControllers((Assembly[])assemblies);
        }

        /// <summary>
        /// Register all classes inherit from <seealso cref="IDependency"/> and its extension
        /// </summary>
        /// <param name="types">Types need to be registered</param>
        /// <param name="builder">Autofac container builder</param>
        private static void RegisterDependencies(IEnumerable<Type> types, ContainerBuilder builder)
        {
            foreach (var type in types.Distinct())
            {
                // Get the direct interface which type extend
                var interfaces = type.GetInterfaces();

                if (type.IsGenericType)
                {
                    var genericRegistration = builder.RegisterGeneric(type);
                    foreach (var @interface in interfaces)
                    {
                        if (@interface.IsGenericType)
                        {
                            genericRegistration = genericRegistration.As(@interface);
                            if (typeof(ISingletonDependency).IsAssignableFrom(@interface))
                            {
                                // Type extend from ISingletonDependency interface,
                                // so register it as Singleton
                                genericRegistration = genericRegistration.SingleInstance();
                            }
                            else if (typeof(IPerRequestDependency).IsAssignableFrom(@interface))
                            {
                                // Type extend from IUnitOfWorkDependency interface,
                                // so register it as Instant per Request
                                genericRegistration = genericRegistration.InstancePerRequest();
                            }
                            else
                            {
                                // Type extend from IDependency interface,
                                // so register it as Instant per Dependency
                                genericRegistration = genericRegistration.InstancePerDependency();
                            }
                        }
                    }
                }
                else
                {
                    var registration = builder.RegisterType(type);

                    foreach (var @interface in interfaces)
                    {
                        registration = registration.As(@interface);

                        if (typeof(ISingletonDependency).IsAssignableFrom(@interface))
                        {
                            // Type extend from ISingletonDependency interface,
                            // so register it as Singleton
                            registration = registration.SingleInstance();
                        }
                        else if (typeof(IPerRequestDependency).IsAssignableFrom(@interface))
                        {
                            // Type extend from IUnitOfWorkDependency interface,
                            // so register it as Instant per Request
                            registration = registration.InstancePerRequest();
                        }
                        else
                        {
                            // Type extend from IDependency interface,
                            // so register it as Instant per Dependency
                            registration = registration.InstancePerDependency();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Register all classes inherit from <seealso cref="System.Reflection.Module"/> class
        /// </summary>
        /// <param name="types">Types need to be registered</param>
        /// <param name="builder">Autofac container builder</param>
        private static void RegisterModules(IEnumerable<Type> types, ContainerBuilder builder)
        {
            foreach (var type in types.Distinct())
            {
                try
                {
                    var instance = Activator.CreateInstance(type) as IModule;
                    if (instance != null)
                    {
                        builder.RegisterModule(instance);
                    }
                }
                catch (Exception exception)
                {
                    throw exception.InnerException ?? exception;
                }
            }
        }
    }
}
