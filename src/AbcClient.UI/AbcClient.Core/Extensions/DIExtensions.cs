using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Core.Extensions
{
    /// <summary>
    /// 服务扩展方法
    /// </summary>
    public static class DIExtensions
    {

        /// <summary>
        /// 添加应用程序默认配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultConfuguration(this IServiceCollection services)
        {
            // 创建配置建造器
            var configurationBuilder = new ConfigurationBuilder();

            // 设置配置文件路径
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // 添加应用程序配置文件
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // 依赖注入
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            return services;
        }
    }
}
