using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Balance;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.View.Balance
{
    public sealed partial class EditBalancePage : Page
    {
        private BalanceViewModel _balance;

        public EditBalancePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _balance = e.Parameter as BalanceViewModel;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(ChangeBalanceTextBox.Text))
            {
                int value;
                bool canChange = Int32.TryParse(ChangeBalanceTextBox.Text, out value);

                if (canChange)
                {
                    _balance.ChanngeBalance(value);
                }
            }

            if (_balance.IsNew)
            {
                _balance.IsNew = false;
                MainViewModel.Current.AddBalance(_balance);
            }
            else
            {
                MainViewModel.Current.UpdateBalance(_balance);
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (!_balance.IsNew)
                MainViewModel.Current.DeleteBalance(_balance);

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
