using piggy_bank_uwp.Workers;
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded)
                return;

            SettingsWorker.Current.SaveRequestedTheme(ApplicationTheme.Light);
            ChangedTextBlock.Visibility = Visibility.Visible;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
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

            _isLoaded = true;
        }
    }
}
