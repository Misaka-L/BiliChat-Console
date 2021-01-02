using BiliChat_Console.Models;
using MaterialDesignThemes.Wpf;
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

namespace BiliChat_Console.UserControls.Dialog
{
    /// <summary>
    /// CustomemotionEditDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CustomemotionEditDialog : UserControl
    {
        public CustomemotionEditDialog(string imageUrl, string command)
        {
            InitializeComponent();
            ImageUrl.Text = imageUrl;
            DanmakuCommand.Text = command;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ImageUrl.Text) && !string.IsNullOrWhiteSpace(DanmakuCommand.Text))
            {
                DialogHost.CloseDialogCommand.Execute(new Customemotion { command = DanmakuCommand.Text, source = ImageUrl.Text }, this);
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, this);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}
