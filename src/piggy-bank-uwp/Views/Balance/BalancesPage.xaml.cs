using piggy_bank_uwp.View.Balance;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Balance;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.Views.Balance
{
    public sealed partial class BalancesPage : Page
    {
        public BalancesPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = MainViewModel.Current.Accounts;
        }

        private void OnBalanceItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(EditBalancePage), e.ClickedItem);
        }

        private void OnAddedBalanceClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditBalancePage), new BalanceViewModel());
        }
    }
}
