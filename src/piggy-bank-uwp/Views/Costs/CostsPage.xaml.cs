using piggy_bank_uwp.View.Balance;
using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.Views.Costs
{
    public sealed partial class CostsPage : Page
    {
        public CostsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CostsListView.ItemsSource = MainViewModel.Current.Costs;

            if (MainViewModel.Current.CanShowToast)
            {
                MainViewModel.Current.ShowToast();
                MainViewModel.Current.SaveLastTimeShow();
            }
        }

        private void OnCostItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(EditCostPage), e.ClickedItem);
        }

        private void OnAddedCategoryClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCostPage), new CostViewModel());
        }

        private void OnNavigateEditBalance(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditBalancePage), MainViewModel.Current.Balance);
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            RefreshContainer.RequestRefresh();
        }

        private async void OnRefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            using (var complion = args.GetDeferral())
            {
                await MainViewModel.Current.FetchCosts();
            }
        }
    }
}
