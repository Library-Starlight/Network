using AbcClient.Core.Datastore;
using AbcClient.Core.DI;
using AbcClient.UI.ViewModel.SocketList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.UI
{
    public class SingleTonInstance<TInstance>
        where TInstance : new()
    {
        /// <summary>
        /// 线程同步锁
        /// </summary>
        private readonly static object _objLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static TInstance _instance;

        public static TInstance Instance
        {
            get
            {
                if (_instance == null)
                    lock (_objLock)
                        if (_instance == null)
                            _instance = new TInstance();
                return _instance;
            }
        }
    }

    public class AwesomeClientListDesignModel : SingleTonInstance<AwesomeClientListViewModel>
    {

        public AwesomeClientListDesignModel()
        {
            Instance.Items = new ObservableCollection<AwesomeClientListItemViewModel>
            {
                new AwesomeClientListClientViewModel(),
                new AwesomeClientListClientViewModel(),
                new AwesomeClientListClientViewModel(),
                new AwesomeClientListClientViewModel(),
            };
        }
    }
}
