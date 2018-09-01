using piggy_bank_uwp.View.Balance;
using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using piggy_bank_uwp.Services;

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

            if (MainViewModel.Current.Costs.GetEnumerator().MoveNext())
            {
                CostsListView.ItemsSource = MainViewModel.Current.Costs;
                StupCollapsed();
            }
            else
            {
                StupVisible();
            }

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

        private async void OnAddedCostClick(object sender, RoutedEventArgs e)
        {
            if (!MainViewModel.Current.HaveCategories)
            {
                await DialogService
                    .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringCategoriesContent))
                    .ShowAsync();

                return;
            }

            Frame.Navigate(typeof(EditCostPage), new CostViewModel());
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

        private void StupVisible()
        {
            RefreshContainer.Visibility = Visibility.Collapsed;
            StubTextBlock.Visibility = Visibility.Visible;
        }

        private void StupCollapsed()
        {
            RefreshContainer.Visibility = Visibility.Visible;
            StubTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}
