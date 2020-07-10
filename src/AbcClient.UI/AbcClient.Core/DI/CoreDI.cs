using AbcClient.Core.Datastore;
using AbcClient.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Core.DI
{
    public class CoreDI
    {
        /// <summary>
        /// 服务集合
        /// </summary>
        public static IServiceCollection Services { get; } = new ServiceCollection();

        /// <summary>
        /// 服务解析
        /// </summary>
        public static IServiceProvider ServiceProvider { get; protected set; }

        /// <summary>
        /// 应用程序配置
        /// </summary>
        public static IConfiguration Configuration { get; protected set; }

        /// <summary>
        /// 程序初始化设置
        /// </summary>
        /// <returns></returns>
        public static async Task SetupAsync()
        {
            var sp = Services.BuildServiceProvider();
            // 获取配置项
            Configuration = sp.GetService<IConfiguration>();
            // 添加数据库上下文
            Services.AddDbContext<AbcDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            sp = Services.BuildServiceProvider();
            var db = sp.GetService<AbcDbContext>();

            await db.Database.EnsureCreatedAsync();

            // 初始化所有基础服务后，创建服务提供器
            ServiceProvider = Services.BuildServiceProvider();
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
