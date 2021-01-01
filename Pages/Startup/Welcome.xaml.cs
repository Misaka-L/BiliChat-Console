using System.Windows;
using System.Windows.Controls;

namespace BiliChat_Console.Pages.Startup
{
    /// <summary>
    /// Welcome.xaml 的交互逻辑
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.NavigateToPage(new Runtime());
        }
    }
}
