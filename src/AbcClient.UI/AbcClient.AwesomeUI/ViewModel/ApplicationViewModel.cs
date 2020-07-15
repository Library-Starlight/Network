using AbcClient.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.AwesomeUI
{
    public class ApplicationViewModel : BaseViewModel
    {
        /// <summary>
        /// 当前选中页面
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        /// <summary>
        /// 当前页面的视图模型
        /// </summary>
        public BaseViewModel CurrentViewModel { get; set; }

        /// <summary>
        /// 切换到指定页面
        /// </summary>
        /// <param name="applicationPage"></param>
        /// <param name="viewModel"></param>
        public void GoToPage(ApplicationPage applicationPage, BaseViewModel viewModel = null)
        {

        }
    }
}
