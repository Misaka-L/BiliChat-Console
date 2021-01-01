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
    /// InputTextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputTextDialog : UserControl
    {
        public InputTextDialog(string message)
        {
            InitializeComponent();
            DialogMessage.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(DialogInputText.Text, this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}
