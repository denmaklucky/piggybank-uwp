using piggy_bank_uwp.ViewModels.Donate;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.View.Donate
{
    public sealed partial class DonatePage : Page
    {
        private DonateViewModel _donate;

        public DonatePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _donate = e.Parameter as DonateViewModel;
            ListViewDonate.ItemsSource = _donate.Items;
        }

        private async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            DonateItemViewModel selectedItem = e.ClickedItem as DonateItemViewModel;

            if (selectedItem == null)
                return;

            await _donate.BuyItem(selectedItem);
        }
    }
}
