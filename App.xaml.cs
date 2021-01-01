using BiliChat_Console.Models;
using Hardcodet.Wpf.TaskbarNotification;
using LitJson;
using System.IO;
using System.Windows;

namespace BiliChat_Console
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static ViewModel ViewModel = new ViewModel();
        public static TaskbarIcon Taskbar;
        public static Settings Settings = new Settings();

        protected override void OnStartup(StartupEventArgs e)
        {
            Settings = JsonMapper.ToObject<Settings>(File.ReadAllText("config.json"));
            Taskbar = (TaskbarIcon)FindResource("Taskbar");
            base.OnStartup(e);
        }
    }
}
