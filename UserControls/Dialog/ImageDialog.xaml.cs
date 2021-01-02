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
    /// ImageDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImageDialog : UserControl
    {
        public ImageDialog(ImageSource source)
        {
            InitializeComponent();
            ImageContent.Source = source;
        }

        private void Mian_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }

        private void Mian_KeyDown(object sender, KeyEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}