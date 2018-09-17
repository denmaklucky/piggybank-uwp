using piggy_bank_uwp.Services;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
using piggy_bank_uwp.ViewModel.Tag;
using piggy_bank_uwp.ViewModels.Balance;
using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace piggy_bank_uwp.View.Costs
{
    public sealed partial class EditCostPage : Page
    {
        private CostViewModel _cost;
        private bool _isInit;

        public EditCostPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _cost = e.Parameter as CostViewModel;
            DatePicker.Date = _cost.DateOffset;
            CategoriesComboBox.ItemsSource = MainViewModel.Current.Categories;
            BalancesComboBox.ItemsSource = MainViewModel.Current.Accounts.Balances;

            if (!_cost.IsNew)
            {
                CategoriesComboBox.SelectedItem = MainViewModel.Current.Categories.FirstOrDefault(t => t.Id == _cost.CategoryId);
                BalancesComboBox.SelectedItem = MainViewModel.Current.Accounts.Balances.FirstOrDefault(b=>b.Id == _cost.BalanceId);
                CostTextBox.Text = _cost.Cost.ToString();
            }

            _isInit = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            _isInit = false;
        }

        private async void OnSaveClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(CategoriesComboBox.SelectedItem == null)
            {
                await DialogService
                    .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringCostContent))
                    .ShowAsync();

                return;
            }

            if(BalancesComboBox.SelectedItem == null)
            {
                await DialogService
                    .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringBalanceCostContent))
                    .ShowAsync();
                return;
            }

            if (_cost.IsNew)
            {
                _cost.IsNew = false;
                await MainViewModel.Current.AddCost(_cost);
            }
            else
            {
                await MainViewModel.Current.UpdateCost(_cost);
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
            if (!_isInit)
                return;

            var selectedCategory = e.AddedItems[0] as CategoryViewModel;

            _cost.ChangedCategory(selectedCategory?.Id);
        }

        private async void OnDeleteClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(!_cost.IsNew)
                await MainViewModel.Current.DeleteCost(_cost);

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate == null)
                return;

            _cost.DateOffset = args.NewDate.Value;
        }

        private void OnCostTextChanged(object sender, TextChangedEventArgs e)
        {
           if (String.IsNullOrEmpty(CostTextBox.Text))
                return;

            int value;
            bool canSet = Int32.TryParse(CostTextBox.Text, out value);

            if (canSet)
            {
                _cost.Cost = value;
            }
            else
            {
                _cost.Cost = 0;
            }
        }

        private void OnBalanceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isInit)
                return;

            var selectedBalance = e.AddedItems[0] as BalanceViewModel;

            _cost.ChangedBalance(selectedBalance?.Id);
        }
    }
}
