using AbcClient.Core.Datastore;
using AbcClient.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AbcClient.UI.ViewModel.SocketList
{
    public class AwesomeClientListViewModel : BaseViewModel
    {
        #region 公共属性

        /// <summary>
        /// 列表项
        /// </summary>
        public ObservableCollection<AwesomeClientListItemViewModel> Items { get; set; }

        #endregion

        #region 命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public ICommand LoadCommand { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public AwesomeClientListViewModel()
        {
            // 初始化命令
            LoadCommand = new RelayCommand(async () => await LoadAsync());

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        private Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
