using LitJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliChat_Console.Models
{

    public class Settings
    {
        private bool _loadAvatar { get; set; } = true;
        public bool loadAvatar {
            get
            {
                return _loadAvatar;
            }
            set
            {
                _loadAvatar = value;
                SaveSettings();
            }
        }

        private int _levelFilter { get; set; } = 0;
        public int levelFilter
        {
            get
            {
                return _levelFilter;
            }
            set
            {
                _levelFilter = value;
                SaveSettings();
            }
        }

        private string[] _wordFilter { get; set; }
        public string[] wordFilter
        {
            get
            {
                return _wordFilter;
            }
            set
            {
                _wordFilter = value;
                SaveSettings();
            }
        }

        private bool _showGift { get; set; } = true;
        public bool showGift
        {
            get
            {
                return _showGift;
            }
            set
            {
                _showGift = value;
                SaveSettings();
            }
        }

        private bool _hideGiftDanmaku { get; set; } = true;
        public bool hideGiftDanmaku
        {
            get
            {
                return _hideGiftDanmaku;
            }
            set
            {
                _hideGiftDanmaku = value;
                SaveSettings();
            }
        }

        private bool _groupSimilar { get; set; } = true;
        public bool groupSimilar
        {
            get
            {
                return _groupSimilar;
            }
            set
            {
                _groupSimilar = value;
                SaveSettings();
            }
        }

        private int _groupSimilarWindow { get; set; } = 5;
        public int groupSimilarWindow
        {
            get
            {
                return _groupSimilarWindow;
            }
            set
            {
                _groupSimilarWindow = value;
                SaveSettings();
            }
        }

        private int _maxDanmakuNum { get; set; } = 100;
        public int maxDanmakuNum
        {
            get
            {
                return _maxDanmakuNum;
            }
            set
            {
                _maxDanmakuNum = value;
                SaveSettings();
            }
        }

        private DisplayMode _displayMode { get; set; } = DisplayMode.All;
        public DisplayMode displayMode
        {
            get
            {
                return _displayMode;
            }
            set
            {
                _displayMode = value;
                SaveSettings();
            }
        }

        private int[] _blackList { get; set; }
        public int[] blackList
        {
            get
            {
                return _blackList;
            }
            set
            {
                _blackList = value;
                SaveSettings();
            }
        }

        private int _minGiftValue { get; set; } = 20;
        public int minGiftValue
        {
            get
            {
                return _minGiftValue;
            }
            set
            {
                _minGiftValue = value;
                SaveSettings();
            }
        }

        private int _silverGiftRatio { get; set; } = 0;
        public int silverGiftRatio
        {
            get
            {
                return _silverGiftRatio;
            }
            set
            {
                _silverGiftRatio = value;
                SaveSettings();
            }
        }

        private Customemotion[] _customEmotions { get; set; }
        public Customemotion[] customEmotions
        {
            get
            {
                return _customEmotions;
            }
            set
            {
                _customEmotions = value;
                SaveSettings();
            }
        }

        private string _biliChat_Console_BackgroundImsgeUrl { get; set; } = "";
        public string BiliChat_Console_BackgroundImageUrl
        {
            get
            {
                return _biliChat_Console_BackgroundImsgeUrl;
            }
            set
            {
                _biliChat_Console_BackgroundImsgeUrl = value;
                SaveSettings();
            }
        }

        private double _biliChat_Console_Opacity { get; set; } = 1;
        public double BiliChat_Console_Opacity
        {
            get
            {
                return _biliChat_Console_Opacity;
            }
            set
            {
                _biliChat_Console_Opacity = value;
                SaveSettings();
            }
        }

        public Settings()
        {
            if (!File.Exists("config.json"))
            {
                SaveSettings();
            }
        }

        public bool SaveSettings()
        {
            try
            {
                var fileStream = File.CreateText("config.json");
                fileStream.WriteLine(JsonMapper.ToJson(this));
                fileStream.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class Customemotion
    {
        public string command { get; set; }
        public string source { get; set; }
    }

    public enum DisplayMode
    {
        None = 0,
        OnlyDanmaku = 1,
        OnlyGift = 2,
        All = 3
    }
}
