using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace piggy_bank_uwp.View
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private MainViewModel mainViewModel;

		public MainPage()
		{
			this.InitializeComponent();

			mainViewModel = new MainViewModel();
		}

		private void PageLoaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void NavigateSettingPage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(SettingPage));
		}

		private void AddNewCost(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditCostPage));
		}

		private void NavigateDonatePage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(DonatePage));
		}

		private void NavigateEditBalancePage(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditBalancePage));
		}

		private void NavigateDiagramPage(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(DiagramPage));
		}

		private void NavigateCostPage(object sender, ItemClickEventArgs e)
		{
			Frame.Navigate(typeof(CostPage));
		}
	}
}
