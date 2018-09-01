using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Tag;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.Views.Categories
{
    public sealed partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CategoriesListView.ItemsSource = MainViewModel.Current.Categories;
        }

        private void OnAddedCategoryClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCategoryPage), new CategoryViewModel());
        }

        private void OnCategoryItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(EditCategoryPage), e.ClickedItem);
        }
    }
}
