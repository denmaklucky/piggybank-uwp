using piggy_bank_uwp.ExtensionMethods;
using piggy_bank_uwp.Services;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Tag;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System;

namespace piggy_bank_uwp.Views.Categories
{

    public sealed partial class EditCategoryPage : Page
    {
        private CategoryViewModel _category;

        public EditCategoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _category = e.Parameter as CategoryViewModel;

            if (!_category.IsNew)
            {
                foreach (Ellipse item in ColorsGridView.Items)
                {
                    if (item.Tag.ToString() == _category.Color)
                        ColorsGridView.SelectedItem = item;
                }
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if(!_category.IsNew)
                MainViewModel.Current.DeleteCategory(_category);

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if(ColorsGridView.SelectedItem == null)
            {
                await DialogService
                    .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringCategoryContent))
                    .ShowAsync();

                return;
            }

            if (_category.IsNew)
            {
                _category.IsNew = false;
                await MainViewModel.Current.AddCategory(_category);
            }
            else
            {
               await MainViewModel.Current.UpdateCategory(_category);
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnColorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemBrush = (e.AddedItems[0] as Ellipse).Fill as SolidColorBrush;
            _category.Color = selectedItemBrush.ToColor();
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
