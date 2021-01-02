using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace BiliChat_Console.Pages.Startup
{
    public partial class Runtime : Page
    {
        public Runtime()
        {
            InitializeComponent();
            if (Directory.Exists("bin"))
            {
                if (!File.Exists("bin/node.exe"))
                {
                    FullDeploy();
                }

                NodeJsStatueText.Text = "完成";
                NodeJsProgress.Value = NodeJsProgress.Maximum;

                if (!File.Exists("bin/bilichat.cmd"))
                {
                    DeployBiliChat();
                }

                BiliChatStatueText.Text = "完成";
                BiliChatProgress.Value = NodeJsProgress.Maximum;

                NextButton.IsEnabled = true;
            }
            else
            {
                Directory.CreateDirectory("bin");
                FullDeploy();
            }
        }

        private void FullDeploy()
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    NodeJsStatueText.Text = "下载中...";
                }));

                string tempPath = Path.GetTempFileName();
                if (Environment.Is64BitOperatingSystem)
                {
                    DownloadHttpFile("https://npm.taobao.org/mirrors/node/v14.15.3/node-v14.15.3-win-x64.zip", tempPath, NodeJsProgress);
                }
                else
                {
                    DownloadHttpFile("https://npm.taobao.org/mirrors/node/v14.15.3/node-v14.15.3-win-x86.zip", tempPath, NodeJsProgress);
                }

                Dispatcher.Invoke(new Action(delegate
                {
                    NodeJsProgress.IsIndeterminate = true;
                    NodeJsStatueText.Text = "解压中...";
                }));

                var zip = ZipFile.Open(tempPath, ZipArchiveMode.Read);
                Dispatcher.Invoke(new Action(delegate
                {
                    NodeJsProgress.Maximum = zip.Entries.Count;
                    NodeJsProgress.Value = 0;
                    NodeJsProgress.IsIndeterminate = false;
                }));

                if (!Directory.Exists("bin"))
                {
                    Directory.CreateDirectory("bin/node");
                }

                for (int i = 1; i != zip.Entries.Count; i++)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        NodeJsStatueText.Text = $"解压中... ({ zip.Entries.Count.ToString() }/{ i.ToString() })";
                        NodeJsProgress.Value = i;
                    }));

                    string path = zip.Entries[i].FullName.Replace(zip.Entries.First().FullName, "");
                    if (path.EndsWith("/"))
                    {
                        Directory.CreateDirectory($"bin/{ path }");
                    }
                    else
                    {
                        if (!File.Exists(path))
                        {
                            File.Create($"bin/{ path }").Close();
                        }

                        zip.Entries[i].ExtractToFile($"bin/{ path }", true);
                    }
                }

                zip.Dispose();
                File.Delete(tempPath);
                Dispatcher.Invoke(new Action(delegate
                {
                    NodeJsProgress.IsIndeterminate = false;
                    NodeJsProgress.Value = NodeJsProgress.Maximum;
                    NodeJsStatueText.Text = "完成";
                }));

                DeployBiliChat();
            }, null);
        }

        private void DeployBiliChat()
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    BiliChatStatueText.Text = "正在安装...";
                    BiliChatProgress.IsIndeterminate = true;
                }));

                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                process.StandardInput.WriteLine(@".\bin\npm install -g bilichat --registry=https://registry.npm.taobao.org" + "&exit");
                process.StandardInput.AutoFlush = true;

                string strOuput = process.StandardOutput.ReadToEnd();
                Debug.WriteLine(strOuput);
                process.WaitForExit();
                process.Close();

                Dispatcher.Invoke(new Action(delegate
                {
                    BiliChatStatueText.Text = "完成";
                    BiliChatProgress.IsIndeterminate = false;
                    BiliChatProgress.Value = BiliChatProgress.Maximum;

                    NextButton.IsEnabled = true;
                }));
            }, null);
        }

        public void DownloadHttpFile(string http_url, string save_url, ProgressBar progressBar)
        {
            WebResponse response;
            // 获取远程文件
            WebRequest request = WebRequest.Create(http_url);

            response = request.GetResponse();
            if (response == null) return;

            // 读远程文件的大小
            progressBar.Dispatcher.Invoke(new Action(delegate
            {
                progressBar.Maximum = response.ContentLength;
            }));

            // 下载远程文件
            Stream netStream = response.GetResponseStream();
            Stream fileStream = new FileStream(save_url, FileMode.Create);

            byte[] read = new byte[1024];
            long progressBarValue = 0;
            int realReadLen = netStream.Read(read, 0, read.Length);
            while (realReadLen > 0)
            {
                fileStream.Write(read, 0, realReadLen);
                progressBarValue += realReadLen;
                progressBar.Dispatcher.Invoke(new Action(delegate
                {
                    progressBar.Value = progressBarValue;
                }));
                realReadLen = netStream.Read(read, 0, read.Length);
            }
            netStream.Close();
            fileStream.Close();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.NavigateToPage(new Finish());
        }
    }
}
