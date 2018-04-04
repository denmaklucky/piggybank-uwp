using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
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
			TagsComboBox.ItemsSource = MainViewModel.Current.Tags;
			TagsComboBox.SelectedIndex = 0;
		}

		private void OnSaveClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				Frame.GoBack();
			}
		}
	}
}
