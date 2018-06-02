using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using piggy_bank_uwp.Workers;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace piggy_bank_uwp.ViewModels.Services
{
    public class OneDriveViewModel
    {
        private const string APP_ID = "9901944b-cca0-4e61-ad86-2ce5a593ad51";

        private OneDriveClient _oneDriveClient;
        private MsaAuthenticationProvider _msaAuthenticationProvider;
        private CredentialCache _credentialCache;
        private string[] _scopes;

        public OneDriveViewModel()
        {
            _scopes = new string[] { "wl.signin", "onedrive.readwrite" };
            IsAuthenticated = false;
            _credentialCache = new CredentialCache();
        }

        public async Task Login()
        {
            try
            {
                _msaAuthenticationProvider = new MsaAuthenticationProvider(APP_ID, ReturnURL, _scopes);

                await _msaAuthenticationProvider.AuthenticateUserAsync();

                _credentialCache = _msaAuthenticationProvider.CredentialCache;

                _oneDriveClient = new OneDriveClient(BaseURL, _msaAuthenticationProvider);

                IsAuthenticated = _msaAuthenticationProvider.IsAuthenticated;
            }
            catch { }
        }

        public async Task ResotreAuthenticateUser()
        {
            byte[] cacheBlod = SettingsWorker.Current.GetCahceBlod();

            if (cacheBlod == null)
                return;

            _credentialCache.InitializeCacheFromBlob(cacheBlod);
            _msaAuthenticationProvider = new MsaAuthenticationProvider(APP_ID, ReturnURL, _scopes, _credentialCache);

            try
            {
                await _msaAuthenticationProvider.RestoreMostRecentFromCacheAsync();

                if (_msaAuthenticationProvider.IsAuthenticated)
                {
                    _oneDriveClient = new OneDriveClient(BaseURL, _msaAuthenticationProvider);
                    IsAuthenticated = true;
                }
            }
            catch { }
        }

        public async Task Logout()
        {
            try
            {
                if (_msaAuthenticationProvider.IsAuthenticated)
                {
                    await _msaAuthenticationProvider.SignOutAsync();
                    IsAuthenticated = false;
                }
            }
            catch { }
        }

        internal void SaveNotificationSetting(bool isOn)
        {
            SettingsWorker.Current.SaveNotificationSetting(isOn);
        }

        public async Task CreateData()
        {
            var folder = new Item { Name = Constants.appName, Folder = new Folder() };
            try
            {
                var item = await _oneDriveClient.Drive.Root.Children.Request().AddAsync(folder);
                SettingsWorker.Current.SaveFolderId(item?.Id);
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("Costs.db");

                using (Stream contentStream = await file.OpenStreamForReadAsync())
                {
                    await _oneDriveClient.
                        Drive.Root.ItemWithPath(Constants.appName + '/' + file.Name).
                        Content.Request().
                        PutAsync<Item>(contentStream);
                }
            }
            catch { }
        }

        public async Task<bool> UpdateData()
        {
            bool haveData = false;
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("Costs.db");

                using (Stream contentStream = await file.OpenStreamForReadAsync())
                {
                    await _oneDriveClient.
                        Drive.Root.ItemWithPath(Constants.appName + '/' + file.Name).
                        Content.Request().
                        PutAsync<Item>(contentStream);
                }

                haveData = true;
            }
            catch { }

            return haveData;
        }

        public async Task DeleteData()
        {
            string folderId = SettingsWorker.Current.GetFolderId();

            if (String.IsNullOrEmpty(folderId))
                return;

            try
            {
                await _oneDriveClient.Drive.Items[folderId].Request().DeleteAsync();
                SettingsWorker.Current.SaveFolderId(String.Empty);
            }
            catch { }
        }

        public async Task<bool> DonwloadData()
        {
            bool haveData = false;
            try
            {
                Item item = await _oneDriveClient
                 .Drive
                 .Root
                 .ItemWithPath("PiggyBank/Costs.db")
                 .Request()
                 .GetAsync();

                using (Stream contentStream = await _oneDriveClient
                                   .Drive
                                   .Items[item.Id]
                                   .Content
                                   .Request()
                                   .GetAsync())
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.
                                                        CreateFileAsync("Costs.db", CreationCollisionOption.OpenIfExists);

                    using (Stream outputstream = await file.OpenStreamForWriteAsync())
                    {
                        await contentStream.CopyToAsync(outputstream);
                    }
                }
                haveData = true;
            }
            ///TODO:
            catch{ }

            return haveData;
        }

        public void SaveCacheBlod() => SettingsWorker.Current.SaveCacheBlod(_credentialCache?.GetCacheBlob());

        public void ClrearCacheBlod() => SettingsWorker.Current.SaveCacheBlod(null);

        public void SaveLastTimeShow() => SettingsWorker.Current.SaveLastTimeShow(DateTime.UtcNow);

        public void ClearLastTimeShow() => SettingsWorker.Current.SaveLastTimeShow(null);

        public bool IsAuthenticated { get; private set; }

        public string ReturnURL
        {
            get
            {
                return "https://login.live.com/oauth20_desktop.srf";
            }
        }

        public string BaseURL
        {
            get
            {
                return "https://api.onedrive.com/v1.0";
            }
        }

        public bool IsNotificationOn => SettingsWorker.Current.GetNotificatinsSetting();
    }
}
