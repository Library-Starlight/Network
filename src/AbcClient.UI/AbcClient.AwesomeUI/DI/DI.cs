using static AbcClient.Core.DI.CoreDI;

namespace AbcClient.AwesomeUI
{
    public static class DI
    {
        /// <summary>
        /// 应用程序视图模型
        /// </summary>
        public static ApplicationViewModel ViewModelApplication => GetService<ApplicationViewModel>();
    }
}
