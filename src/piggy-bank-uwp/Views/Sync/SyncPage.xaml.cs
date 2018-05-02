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
        public SyncPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _oneDrive = MainViewModel.Current.OneDrive;

            await _oneDrive.ResotreAuthenticateUser();

            if (_oneDrive.IsAuthenticated)
            {
                ShowLogoutButton();
            }
            else
            {
                ShowLoginButton();
            }
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

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            await _oneDrive.Login();

            if (_oneDrive.IsAuthenticated)
            {
                ShowLogoutButton();
                _oneDrive.SaveCacheBlod();
                await _oneDrive.CreateData();
            }
            else
            {
                ShowLoginButton();
            }
        }

        private async void OnLogoutClick(object sender, RoutedEventArgs e)
        {
             await _oneDrive.Logout();

            if (_oneDrive.IsAuthenticated)
            {
                ShowLogoutButton();
            }
            else
            {
                ShowLoginButton();
                _oneDrive.ClrearCacheBlod();
            }
        }
    }
}
