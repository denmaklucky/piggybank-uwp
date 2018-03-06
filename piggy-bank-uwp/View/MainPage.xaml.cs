using piggy_bank_uwp.Controls.MasterDetailView;
using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace piggy_bank_uwp.View
{	
	public sealed partial class MainPage : Page
	{
		private MainViewModel mainViewModel;

		public MainPage()
		{
			this.InitializeComponent();

			mainViewModel = new MainViewModel();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			mainViewModel.Init();

			DataContext = mainViewModel;
			ShowStart();
			SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
		}

		private void NavigateSettingPage(object sender, RoutedEventArgs e)
		{
			ShowSetting();
		}

		private void AddNewCost(object sender, RoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(EditCostPage), null);
		}

		private void NavigateDonatePage(object sender, RoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(DonatePage), null);
		}

		private void NavigateEditBalancePage(object sender, RoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(EditBalancePage), null);
		}

		private void NavigateDiagramPage(object sender, TappedRoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(DiagramPage), null);
		}

		private void NavigateCostPage(object sender, ItemClickEventArgs e)
		{
			MainContainer.Navigate(typeof(CostPage), null);
		}

		private void OnStateChanged(object sender, MasterDetailState e)
		{

		}

		private void ShowStart()
		{
			StartGrid.Visibility = Visibility.Visible;
			Setting.Visibility = Visibility.Collapsed;
		}

		private void ShowSetting()
		{
			StartGrid.Visibility = Visibility.Collapsed;
			Setting.Visibility = Visibility.Visible;
		}

		private void OnBackRequested(object sender, BackRequestedEventArgs e)
		{
			if(StartGrid.Visibility == Visibility.Collapsed)
			{
				ShowStart();
			}
		}

		private void OnCostItemClick(object sender, ItemClickEventArgs e)
		{
			MainContainer.Navigate(typeof(CostPage));
		}
	}
}
