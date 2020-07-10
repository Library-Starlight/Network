using AbcClient.Core.Datastore;
using AbcClient.Core.DI;
using AbcClient.Core.Extensions;
using AbcClient.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static AbcClient.Core.DI.CoreDI;

namespace AbcClient.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 应用程序启动设置
            await ApplicationSetupAsync(Services);

            Current.MainWindow = CoreDI.ServiceProvider.GetService<MainWindow>();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// 应用程序启动设置
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationSetupAsync(IServiceCollection services)
        {
            // 添加默认配置项
            services.AddDefaultConfuguration();

            services.AddSingleton<MainWindow>();

            await SetupAsync();
        }
    }
}
