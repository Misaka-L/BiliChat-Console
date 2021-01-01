using System.Windows;
using System.Windows.Controls;

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
