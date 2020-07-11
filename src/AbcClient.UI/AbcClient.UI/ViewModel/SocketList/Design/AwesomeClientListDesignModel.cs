using AbcClient.Core.Datastore;
using AbcClient.Core.DI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.UI
{
    public class AwesomeClientListDesignModel : AwesomeClientListViewModel
    {
        /// <summary>
        /// 线程同步锁
        /// </summary>
        private readonly static object _objLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static AwesomeClientListDesignModel _instance;

        public static AwesomeClientListDesignModel Instance
        {
            get
            {
                if (_instance == null)
                    lock (_objLock)
                        if (_instance == null)
                            _instance = new AwesomeClientListDesignModel();
                return _instance;
            }
        }
        public AwesomeClientListDesignModel()
        {
            Items = new ObservableCollection<AwesomeClientListItemViewModel>
            {
                new AwesomeClientListClientViewModel(),
                new AwesomeClientListClientViewModel(),
                new AwesomeClientListClientViewModel(),
                new AwesomeClientListClientViewModel(),
            };
        }
    }
}
