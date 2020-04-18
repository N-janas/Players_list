using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Lab_1_app
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {

        #region Properties
        public static readonly DependencyProperty IsToolTipShownProperty =
        DependencyProperty.Register(
            "IsToolTipShown",
            typeof(bool),
            typeof(TextBoxWithErrorProvider),
            new FrameworkPropertyMetadata(null)
        );
        public bool IsToolTipShown
        {
            get { return (bool)GetValue(IsToolTipShownProperty); }
            set { SetValue(IsToolTipShownProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBoxWithErrorProvider),
                new FrameworkPropertyMetadata(null)
            );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
        }

    }
}
