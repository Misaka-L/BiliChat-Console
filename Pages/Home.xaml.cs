using BiliChat_Console.Models;
using BiliChat_Console.UserControls.Dialog;
using Hardcodet.Wpf.TaskbarNotification;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BiliChat_Console.Pages
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Page
    {
        Process process = new Process();

        public Home()
        {
            InitializeComponent();

            // 载入设置
            BackgroundUrl.Text = App.Settings.BiliChat_Console_BackgroundImageUrl;
            OpacitySlider.Value = App.Settings.BiliChat_Console_Opacity;
            GroupSimilar.IsChecked = App.Settings.groupSimilar;
            LoadAvatar.IsChecked = App.Settings.loadAvatar;
            GroupSimilarWindow.Text = App.Settings.groupSimilarWindow.ToString();
            MaxDanmakuNum.Text = App.Settings.maxDanmakuNum.ToString();
            hideGiftDanmaku.IsChecked = App.Settings.hideGiftDanmaku;
            ShowGift.IsChecked = App.Settings.showGift;
            DisplayMode.SelectedIndex = (int)App.Settings.displayMode;
            LevelFilter.Text = App.Settings.levelFilter.ToString();
            MinGiftValue.Text = App.Settings.minGiftValue.ToString();
            WordFilterItemsControl.ItemsSource = App.Settings.wordFilter;
            CustomEmotionsItemsControl.ItemsSource = App.Settings.customEmotions;


            process.StartInfo.FileName = @".\bin\node.exe";
            process.StartInfo.Arguments = @".\bin\node_modules\bilichat\dist\server.js";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;
            // 重定向输出输入流
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            // 1202 年了，人类还是没有解决编码问题
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8;

            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_OutputDataReceived; ;
            process.Exited += Process_Exited;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (process.HasExited)
                {
                    StartBiliChat();
                }
                else
                {
                    CloseBiliChat();
                }
            }
            catch
            {
                StartBiliChat();
            }
        }

        private void StartBiliChat()
        {
            Output.Text = "";

            StartButtonText.Text = "关闭";
            StartButtonIcon.Kind = PackIconKind.PaperAirplane;
            BiliChatUrl.Text = "http://localhost:4000/gkd/<直播间ID>";
            BiliChatUrl.IsEnabled = true;
            CopyUrl.IsEnabled = true;
            SettingsPanel.IsEnabled = false;

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
        }

        private void CloseBiliChat()
        {
            if (!process.HasExited)
            {
                process.Kill();
            }

            process.CancelErrorRead();
            process.CancelOutputRead();

            StartButtonText.Text = "启动";
            StartButtonIcon.Kind = PackIconKind.PaperAirplane;
            BiliChatUrl.Text = "请先启动 BiliChat";
            BiliChatUrl.IsEnabled = false;
            CopyUrl.IsEnabled = false;
            SettingsPanel.IsEnabled = true;

            App.Taskbar.ShowBalloonTip("BiliChat 已退出", "BiliChat 已退出", BalloonIcon.Info);
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                CloseBiliChat();
            }));
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    Output.Text = Output.Text + e.Data + Environment.NewLine;
                }));

                if (e.Data.Trim().StartsWith("bilichat正运行在 "))
                {
                    App.Taskbar.ShowBalloonTip("BiliChat 已启动", "BiliChat 正运行在: http://localhost:4000", BalloonIcon.Info);
                }
                if (e.Data.Trim().StartsWith("Error: listen EADDRINUSE: address already in use :::4000"))
                {
                    App.Taskbar.ShowBalloonTip("BiliChat 崩溃了", "请检查端口 4000 是否被使用", BalloonIcon.Error);
                }
                else if (e.Data.Trim().StartsWith("Error:"))
                {
                    App.Taskbar.ShowBalloonTip("BiliChat 崩溃了", "发生了未知错误，请查看输出", BalloonIcon.Error);
                }
            }
        }

        private void CopyUrl_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(BiliChatUrl.Text);
        }

        #region 设置
        private void BackgroundUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.Settings.BiliChat_Console_BackgroundImageUrl = BackgroundUrl.Text;
        }

        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.Settings.BiliChat_Console_Opacity = OpacitySlider.Value;
        }

        private void GroupSimilar_Checked(object sender, RoutedEventArgs e)
        {
            App.Settings.groupSimilar = true;
        }

        private void GroupSimilar_Unchecked(object sender, RoutedEventArgs e)
        {
            App.Settings.groupSimilar = false;
        }

        private void LoadAvatar_Unchecked(object sender, RoutedEventArgs e)
        {
            App.Settings.loadAvatar = false;
        }

        private void LoadAvatar_Checked(object sender, RoutedEventArgs e)
        {
            App.Settings.loadAvatar = true;
        }

        private void GroupSimilarWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                App.Settings.groupSimilarWindow = int.Parse(GroupSimilarWindow.Text);
            }
            catch { }
        }

        private void MaxDanmakuNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                App.Settings.maxDanmakuNum = int.Parse(MaxDanmakuNum.Text);
            }
            catch { }
        }

        private void hideGiftDanmaku_Checked(object sender, RoutedEventArgs e)
        {
            App.Settings.hideGiftDanmaku = true;
        }

        private void hideGiftDanmaku_Unchecked(object sender, RoutedEventArgs e)
        {
            App.Settings.hideGiftDanmaku = false;
        }

        private void ShowGift_Checked(object sender, RoutedEventArgs e)
        {
            App.Settings.showGift = true;
        }

        private void ShowGift_Unchecked(object sender, RoutedEventArgs e)
        {
            App.Settings.showGift = false;
        }

        private void DisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Settings.displayMode = (DisplayMode)DisplayMode.SelectedIndex;
        }

        private void LevelFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                App.Settings.levelFilter = int.Parse(LevelFilter.Text);
            }
            catch { }
        }

        private void MinGiftValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                App.Settings.minGiftValue = int.Parse(MinGiftValue.Text);
            }
            catch { }
        }
        #endregion

        private async void AddWordFilter_Click(object sender, RoutedEventArgs e)
        {
            var result = await DialogHost.Show(new InputTextDialog("请输入要添加的屏蔽词"));
            if (result != null && (string)result != "")
            {
                List<string> list = new List<string>();
                try
                {
                    list = App.Settings.wordFilter.ToList();
                }
                catch { }

                list.Add((string)result);
                App.Settings.wordFilter = list.ToArray();

                WordFilterItemsControl.ItemsSource = App.Settings.wordFilter;
            }
        }

        private void WordFilterChip_DeleteClick(object sender, RoutedEventArgs e)
        {
            string tag = ((Chip)sender).Tag as string;

            var list = App.Settings.wordFilter.ToList();
            list.Remove(tag);

            App.Settings.wordFilter = list.ToArray();

            WordFilterItemsControl.ItemsSource = App.Settings.wordFilter;
        }

        private async void AddBlackUid_Click(object sender, RoutedEventArgs e)
        {
            var result = await DialogHost.Show(new InputTextDialog("请输入要添加的 Uid"));
            if (result != null)
            {
                List<int> list = new List<int>();
                try
                {
                    list = App.Settings.blackList.ToList();
                }
                catch { }

                list.Add(int.Parse((string)result));

                App.Settings.blackList = list.ToArray();

                BlackListItemsControl.ItemsSource = App.Settings.blackList;
            }
        }

        private void BlackListChip_DeleteClick(object sender, RoutedEventArgs e)
        {
            int tag = (int)((Chip)sender).Tag;

            var list = App.Settings.blackList.ToList();
            list.Remove(tag);

            App.Settings.blackList = list.ToArray();

            BlackListItemsControl.ItemsSource = App.Settings.blackList;
        }

        private void Image_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DialogHost.Show(new ImageDialog(((Image)sender).Source));
        }

        private void AddCustomEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CustomEmotionImageUrl.Text) && !string.IsNullOrWhiteSpace(CustomEmotionDanmakuCommand.Text))
            {
                var list = new List<Customemotion>();
                try
                {
                    list = App.Settings.customEmotions.ToList();
                }
                catch { }

                if (!list.Any(a => a.command == CustomEmotionDanmakuCommand.Text || a.source == CustomEmotionImageUrl.Text))
                {
                    list.Add(new Customemotion { command = CustomEmotionDanmakuCommand.Text, source = CustomEmotionImageUrl.Text });
                    App.Settings.customEmotions = list.ToArray();

                    CustomEmotionsItemsControl.ItemsSource = App.Settings.customEmotions;

                    CustomEmotionImageUrl.Text = "";
                    CustomEmotionDanmakuCommand.Text = "";
                }
            }
        }

        private async void EditCustomemotionButton_Click(object sender, RoutedEventArgs e)
        {
            var list = App.Settings.customEmotions.ToList();
            var tag = (Customemotion)((Button)sender).Tag;
            var result = (Customemotion)await DialogHost.Show(new CustomemotionEditDialog(tag.source, tag.command));
            if (result != null && !list.All(a => a == result))
            {
                for (int i = 0;i != list.Count; i++)
                {
                    if (list[i] == tag)
                    {
                        list[i] = result;
                        break;
                    }
                }

                App.Settings.customEmotions = list.ToArray();
                CustomEmotionsItemsControl.ItemsSource = App.Settings.customEmotions;
            }
        }

        private void DeleteCustomemotionButton_Click(object sender, RoutedEventArgs e)
        {
            var list = App.Settings.customEmotions.ToList();
            list.Remove((Customemotion)((Button)sender).Tag);

            App.Settings.customEmotions = list.ToArray();
            CustomEmotionsItemsControl.ItemsSource = App.Settings.customEmotions;
        }
    }
}
