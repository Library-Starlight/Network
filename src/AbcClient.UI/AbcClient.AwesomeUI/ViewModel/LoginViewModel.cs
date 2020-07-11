using AbcClient.UI.Infrastructure;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AbcClient.AwesomeUI
{
    public class LoginViewModel : BaseViewModel
    {
        #region 命令

        /// <summary>
        /// 登录命令
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// 登录中
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        private async Task LoginAsync()
        {
            await RunCommandAsync(() => this.LoginIsRunning, async () =>
            {
                await Task.Delay(250);
            });
        }

        #endregion
    }
}
