using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
using piggy_bank_uwp.ViewModel.Tag;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace piggy_bank_uwp.View.Costs
{
    public sealed partial class EditCostPage : Page
    {
        private CostViewModel _cost;

        public EditCostPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _cost = e.Parameter as CostViewModel;
            TagsComboBox.ItemsSource = MainViewModel.Current.Categories;

            if (_cost.IsNew)
            {
                TagsComboBox.SelectedIndex = 0;
            }
            else
            {
                TagsComboBox.SelectedItem = MainViewModel.Current.Categories.FirstOrDefault(t => t.Id == _cost.CategoryId);
            }
        }

        private void OnSaveClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //TODO: double opene CostPage;
            if (_cost.IsNew)
            {
                MainViewModel.Current.AddCost(_cost);
            }
            else
            {
                MainViewModel.Current.UpdateCost(_cost);
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnCloseClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Need ask about save cost
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnTagSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _cost.Category = e.AddedItems[0] as CategoryViewModel;
        }

        private void OnDeleteClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainViewModel.Current.DeleteCost(_cost);

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
