using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.Views.Sync
{
    public sealed partial class SyncPage : Page
    {
        private OneDriveViewModel _oneDrive;
        private bool _isLoaded;

        public SyncPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _oneDrive = MainViewModel.Current.OneDrive;

            await _oneDrive.ResotreAuthenticateUser();

            EditVisualMode();
            _isLoaded = true;
        }

        private void EditVisualMode()
        {
            if (_oneDrive.IsAuthenticated)
            {
                ShowLogoutButton();
            }
            else
            {
                ShowLoginButton();
            }

            EditEnableNotificationBlock();
        }

        private void ShowLoginButton()
        {
            LoginButton.Visibility = Visibility.Visible;
            LogoutButton.Visibility = Visibility.Collapsed;
        }

        private void ShowLogoutButton()
        {
            LoginButton.Visibility = Visibility.Collapsed;
            LogoutButton.Visibility = Visibility.Visible;
        }

        private void EditEnableNotificationBlock()
        {
            NotificationSwitch.IsOn = _oneDrive.IsNotificationOn;
        }

        private void DisableNotifactionBlock()
        {
            NotificationSwitch.IsOn = false;
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            AuthorizationRing.IsActive = true;

            await _oneDrive.Login();

            if (_oneDrive.IsAuthenticated)
            {
                _oneDrive.SaveCacheBlod();
                _oneDrive.SaveNotificationSetting(isOn: true);
                MainViewModel.Current.SaveLastTimeShow();
            }

            EditVisualMode();

            AuthorizationRing.IsActive = false;
        }

        private async void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            AuthorizationRing.IsActive = true;

            await _oneDrive.Logout();

            if (!_oneDrive.IsAuthenticated)
            {
                _oneDrive.ClrearCacheBlod();
                _oneDrive.SaveNotificationSetting(isOn: false);
            }

            EditVisualMode();

            AuthorizationRing.IsActive = false;
        }

        private void OnToggled(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded)
                return;

            _oneDrive.SaveNotificationSetting(NotificationSwitch.IsOn);

            if (NotificationSwitch.IsOn)
            {
                _oneDrive.SaveLastTimeShow();
            }
            else
            {
                _oneDrive.ClearLastTimeShow();
            }
        }

        private async void OnUploadClick(object sender, RoutedEventArgs e)
        {
            UpdateProgressBar.Visibility = Visibility.Visible;
            bool haveDate = await _oneDrive.UpdateData();

            if (!haveDate)
            {
                await _oneDrive.CreateData();
                await _oneDrive.UpdateData();
            }

            UpdateProgressBar.Visibility = Visibility.Collapsed;
        }

        private async void OnDownloadClick(object sender, RoutedEventArgs e)
        {
            UpdateProgressBar.Visibility = Visibility.Visible;
            bool haveData = await _oneDrive.DonwloadData();

            if (haveData)
            {
                MainViewModel.Current.UpdateData();
            }
            else
            {
                await _oneDrive.CreateData();
            }

            UpdateProgressBar.Visibility = Visibility.Collapsed;
        }
    }
}
