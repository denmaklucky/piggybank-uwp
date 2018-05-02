﻿using Windows.Storage;
using Windows.UI.Xaml;

namespace piggy_bank_uwp.Workers
{
    public sealed class SettingsWorker
    {
        private const string RequestedTheme = "RequestedTheme";
        private const string CacheBlod = "CacheBlod";
        private const string FolderId = "FolderId";

        private ApplicationDataContainer _localSettings;

        private SettingsWorker()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        public void SaveRequestedTheme(ApplicationTheme theme)
        {
            SaveValue(RequestedTheme, (int)theme);
        }

        public ApplicationTheme GetRequestedTheme()
        {
            var theme = GetValue(RequestedTheme);

            if (theme == null)
                theme = ApplicationTheme.Light;

            return (ApplicationTheme)theme;
        }

        public void SaveCacheBlod(byte[] cacheBlod)
        {
            SaveValue(CacheBlod, cacheBlod);
        }

        public byte[] GetCahceBlod()
        {
            var cacheBlod = GetValue(CacheBlod);

            return cacheBlod as byte[];
        }

        public void SaveFolderId(string id)
        {
            SaveValue(FolderId, id);
        }

        public string GetFolderId()
        {
            return GetValue(FolderId) as string;
        }

        private void SaveValue(string key, object value)
        {
            lock (_localSettings)
            {
                try
                {
                    if (_localSettings.Values.ContainsKey(key))
                    {
                        _localSettings.Values[key] = default(string);
                        _localSettings.Values[key] = value;
                    }
                    else
                    {
                        _localSettings.Values.Add(key, value);
                    }
                }
                catch { }
            }
        }

        private object GetValue(string key)
        {
            object value = null;

            lock (_localSettings)
            {
                if (_localSettings.Values.ContainsKey(key))
                    value = _localSettings.Values[key];
            }

            return value;
        }

        public static SettingsWorker Current = new SettingsWorker();
    }
}
