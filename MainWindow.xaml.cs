using BiliChat_Console.Pages;
using BiliChat_Console.Pages.Startup;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BiliChat_Console
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.ViewModel.MainWindow = this;

            if (!File.Exists("config.json"))
            {
                MainFrame.Navigate(new Welcome());
            }

            if (Directory.Exists("bin"))
            {
                if (!File.Exists("bin/node.exe"))
                {
                    MainFrame.Navigate(new Welcome());
                }

                if (!File.Exists("bin/bilichat.cmd"))
                {
                    MainFrame.Navigate(new Welcome());
                }
                else
                {
                    MainFrame.Navigate(new Home());
                }
            }
            else
            {
                MainFrame.Navigate(new Welcome());
            }
        }
    }
}
