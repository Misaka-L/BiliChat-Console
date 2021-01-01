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

namespace BiliChat_Console.Pages.Startup
{
    /// <summary>
    /// Finish.xaml 的交互逻辑
    /// </summary>
    public partial class Finish : Page
    {
        public Finish()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.NavigateToPage(new Home());
        }
    }
}
