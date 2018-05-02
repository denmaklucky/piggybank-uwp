using piggy_bank_uwp.View.Balance;
using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace piggy_bank_uwp.Views.Costs
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
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
    }
}
