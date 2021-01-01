using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliChat_Console.Models
{
    public class ViewModel
    {
        public MainWindow MainWindow;

        public ViewModel()
        {

        }

        public bool NavigateToPage(object content)
        {
            return MainWindow.MainFrame.Navigate(content);
        }
    }
}
