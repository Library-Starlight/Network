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

namespace AbcClient.UI.Infrastructure
{
    /// <summary>
    /// IconText.xaml 的交互逻辑
    /// </summary>
    public partial class FontAwesomeIconText : UserControl
    {
        #region 依赖属性



        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(string), typeof(FontAwesomeIconText), new PropertyMetadata("\uf075", IconValueChangedCallback, IconValueCoerceCallback));

        private static void IconValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FontAwesomeIconText).IconText.Text = (string)e.NewValue;
        }

        private static object IconValueCoerceCallback(DependencyObject d, object baseValue)
        {
            (d as FontAwesomeIconText).IconText.Text = (string)baseValue;
            return baseValue;
        }

        #endregion


        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FontAwesomeIconText()
        {
            InitializeComponent();
        }

        #endregion
    }
}
