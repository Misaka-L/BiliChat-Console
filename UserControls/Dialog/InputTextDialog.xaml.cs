using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

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
