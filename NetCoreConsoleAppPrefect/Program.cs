using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using $safeprojectname$.IServices;
using $safeprojectname$.Services;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace $safeprojectname$
{
    /// <summary>
    /// 完善Console类型的项目，集成DI
    /// </summary>
    class Program
    {
        private static IConfiguration _configuration;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var host = AppStartup();

            ////Service层代码生成
            //var codeGenerateService = ActivatorUtilities.CreateInstance<CodeGenerateService>(host.Services);
            //codeGenerateService.GenerateBusinessServiceFile("User", "用户");

            var demoService = ActivatorUtilities.CreateInstance<DemoService>(host.Services);
            demoService.Test();

            //var threadDemoService = ActivatorUtilities.CreateInstance<ThredDemoService>(host.Services);
            //threadDemoService.Exec();

        }
       


        #region DI

        /// <summary>
        /// 添加DI
        /// </summary>
        /// <returns></returns>
        static IHost AppStartup()
        {
            var basePath = "".GetProgramDirectory();
            _configuration = new ConfigurationBuilder().SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddUserSecrets<Program>()
                    .Build();

            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.AddLogging(option =>
                            {
                                option.SetMinimumLevel(LogLevel.Information);
                                option.AddNLog(Path.Combine(basePath, "nlog.config"));
                            });

                            services.AddSingleton(typeof(IConfiguration), _configuration);

                            services.BatchRegisterServices(new Assembly[] { Assembly.Load(AppDomain.CurrentDomain.FriendlyName) }, typeof(IBatchDIServicesTag));

                        })
                        .Build();

            return host;

        }

        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error(e.ExceptionObject.ToString());

            Environment.Exit(1);
        }

        #endregion
    }

    /// <summary>
    /// 批量注入服务
    /// </summary>
    static class AddCustomServices
    {
        /// <summary>
        /// 批量注入业务类Services
        /// </summary>
        /// <param name="services">DI服务</param>
        /// <param name="assemblys">需要批量注册的程序集集合</param>
        /// <param name="baseType">基础类/接口</param>
        public static void BatchRegisterServices(this IServiceCollection services, Assembly[] assemblys, Type baseType)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //基类是否是泛型
            var baseTypeIsGeneric = baseType.IsGenericType;

            //所有符合注册条件的类集合
            List<Type> typeList = new List<Type>();
            foreach (var assembly in assemblys)
            {
                //获取所有符合条件的实例类
                if (baseTypeIsGeneric)
                {
                    var types = assembly.GetTypes().Where(p => !p.IsInterface && !p.IsSealed && !p.IsAbstract &&
                                                                p.BaseType != null && p.BaseType.IsGenericType &&
                                                                p.BaseType.GetGenericTypeDefinition() == baseType);
                    if (types.Any()) typeList.AddRange(types);
                }
                else
                {
                    var types = assembly.GetTypes().Where(p => !p.IsInterface && !p.IsSealed && !p.IsAbstract &&
                                                                baseType.IsAssignableFrom(p));
                    if (types != null && types.Count() > 0) typeList.AddRange(types);
                }
            }
            if (typeList.Count() == 0) return;

            //待注册集合,key为实现类，value为抽象接口
            var typeDic = new Dictionary<Type, Type[]>();

            //获取实例类对应的接口
            foreach (var type in typeList)
            {
                //如果基类是泛型，则过滤掉泛型接口(IBaseServices<T>)
                if (baseTypeIsGeneric)
                {
                    var interfaces = type.GetInterfaces().Where(p => !p.IsGenericType).ToArray();
                    typeDic.Add(type, interfaces);
                }
                else//否则过滤掉用于批量注册标识的接口(IBatchDIServicesTag)
                {
                    var interfaces = type.GetInterfaces().Where(p => p != baseType).ToArray();
                    typeDic.Add(type, interfaces);
                }
            }

            if (typeDic.Keys.Count == 0) return;

            foreach (var instanceType in typeDic.Keys)
            {
                var serviceBatchTag = (instanceType.GetCustomAttributes(typeof(ServiceLifetimeAttribute), true)[0] as ServiceLifetimeAttribute);
                if (serviceBatchTag == null) serviceBatchTag = new ServiceLifetimeAttribute(); //默认Scoped

                if (!serviceBatchTag.IsEnabled) continue;

                if (typeDic[instanceType].Length == 0)
                {
                    //针对只有实现类，没有接口的情况
                    switch (serviceBatchTag.Lifetime)
                    {
                        case ServiceLifetime.Singleton://单例
                            services.AddSingleton(instanceType);
                            break;
                        case ServiceLifetime.Scoped: //同一请求上下文都是一个对象
                            services.AddScoped(instanceType);
                            break;
                        case ServiceLifetime.Transient: //瞬时单例，每次访问都是新的对象
                            services.AddTransient(instanceType);
                            break;
                    }
                }
                else
                {
                    //注入实现类和接口
                    foreach (var interfaceType in typeDic[instanceType])
                    {
                        switch (serviceBatchTag.Lifetime)
                        {
                            case ServiceLifetime.Singleton://单例
                                services.AddSingleton(interfaceType, instanceType);
                                break;
                            case ServiceLifetime.Scoped: //同一请求上下文都是一个对象
                                services.AddScoped(interfaceType, instanceType);
                                break;
                            case ServiceLifetime.Transient: //瞬时单例，每次访问都是新的对象
                                services.AddTransient(interfaceType, instanceType);
                                break;
                        }
                    }
                }
            }


        }
    }
}
