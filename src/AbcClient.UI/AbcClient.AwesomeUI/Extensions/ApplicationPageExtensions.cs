using AbcClient.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AbcClient.AwesomeUI
{
    public static class ApplicationPageExtensions
    {
        public static BasePage ToBasePage(this ApplicationPage applicationPage, object viewModel = null)
        {
            switch (applicationPage)
            {
                case ApplicationPage.Login:
                    return new LoginPage(viewModel as LoginViewModel);
                case ApplicationPage.Dashboard:
                    return new DashboardPage(viewModel as DashboardViewModel);
                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> this is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches
            switch (page)
            {
                case LoginPage _:
                    return ApplicationPage.Login;
                case DashboardPage _:
                    return ApplicationPage.Dashboard;
            }

            // Alert developer of issue
            Debugger.Break();
            return default;
        }
    }
}
