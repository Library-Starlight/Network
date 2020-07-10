using AbcClient.Core.Datastore;
using AbcClient.Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AbcClient.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AbcDbContext _dbContext;
        public MainWindow(AbcDbContext dbContext)
        {
            InitializeComponent();

            _dbContext = dbContext;

            // 在构造函数等待似乎会出问题?
            // var clients = _dbContext.Client.ToListAsync().Result;
        }
    }
}
