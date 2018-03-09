using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModel.Cost;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace piggy_bank_uwp.View.Costs
{
	/// <summary>
	/// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
	/// </summary>
	public sealed partial class CostPage : Page
	{
		private CostViewModel _cost;

		public CostPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_cost = e.Parameter as CostViewModel;
		}

		private void OnNavigateEditCost(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditCostPage), _cost);
		}

		private void OnCostDeleted(object sender, RoutedEventArgs e)
		{
			MainViewModel.Current.DeleteCost(_cost);
		}
	}
}
