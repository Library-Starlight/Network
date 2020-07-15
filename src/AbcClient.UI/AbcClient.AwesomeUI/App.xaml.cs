using AbcClient.Core.DI;
using AbcClient.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using static AbcClient.Core.DI.CoreDI;

namespace AbcClient.AwesomeUI
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

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// 应用程序启动设置
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationSetupAsync(IServiceCollection services)
        {
            // 添加默认配置项
            services.AddDefaultConfuguration()
                .AddAwesomeUI();

            await SetupAsync();
        }
    }
}
