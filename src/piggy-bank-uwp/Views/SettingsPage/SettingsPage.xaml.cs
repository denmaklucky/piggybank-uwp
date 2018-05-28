using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.Workers;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace piggy_bank_uwp.Views.SettingsPage
{
    public sealed partial class SettingsPage : Page
    {
        private bool _isLoaded;
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void OnLitghButtonChecked(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded)
                return;

            SettingsWorker.Current.SaveRequestedTheme(ApplicationTheme.Light);
            ChangedTextBlock.Visibility = Visibility.Visible;
        }

        private void OnDarkButtonChecked(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded)
                return;

            SettingsWorker.Current.SaveRequestedTheme(ApplicationTheme.Dark);
            ChangedTextBlock.Visibility = Visibility.Visible;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            switch (SettingsWorker.Current.GetRequestedTheme())
            {
                case ApplicationTheme.Light:
                    LightTheme.IsChecked = true;
                    break;
                case ApplicationTheme.Dark:
                    DarkTheme.IsChecked = true;
                    break;
            }

            VersionTextBlock.Text = SystemUtility.GetVersionApp();

            _isLoaded = true;
        }

        private async void OnHyperlinkButtonClick(object sender, RoutedEventArgs e)
        {
            string url = (sender as HyperlinkButton).Tag?.ToString();

            if (string.IsNullOrEmpty(url))
                return;

            await Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
