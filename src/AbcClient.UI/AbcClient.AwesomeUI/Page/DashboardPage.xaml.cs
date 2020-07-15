using AbcClient.UI.Infrastructure;

namespace AbcClient.AwesomeUI
{
    /// <summary>
    /// DashboardPage.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardPage : BasePage<DashboardViewModel>
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        public DashboardPage(DashboardViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
