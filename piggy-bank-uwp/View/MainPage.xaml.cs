using piggy_bank_uwp.Controls.MasterDetailView;
using piggy_bank_uwp.View.Balance;
using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.View.Diagram;
using piggy_bank_uwp.View.Donate;
using piggy_bank_uwp.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace piggy_bank_uwp.View
{	
	public sealed partial class MainPage : Page
	{
		private MainViewModel _mainViewModel;

		public MainPage()
		{
			this.InitializeComponent();

			_mainViewModel = MainViewModel.Current;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			_mainViewModel.Init();
			DataContext = _mainViewModel;
			OnStateChanged(null, MainContainer.CurrentState);
			SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
		}

		#region Navigation

		#region Setting

		private void OnSettingClick(object sender, RoutedEventArgs e)
		{
			ShowSetting();
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

		#endregion

		private void OnNavigateEditCost(object sender, RoutedEventArgs e)
		{
			//MainContainer.Navigate(typeof(EditCostPage));
			_mainViewModel.AddCost();
		}

		private void OnNavigateDonate(object sender, RoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(DonatePage));
		}

		private void OnNavigateEditBalance(object sender, RoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(EditBalancePage));
		}

		private void OnNavigateDiagram(object sender, TappedRoutedEventArgs e)
		{
			MainContainer.Navigate(typeof(DiagramPage));
		}

		private void OnNavigateCost(object sender, ItemClickEventArgs e)
		{
			MainContainer.Navigate(typeof(CostPage), e.ClickedItem);
		}

		#endregion

		private void OnStateChanged(object sender, MasterDetailState e)
		{
			if(e == MasterDetailState.Narrow)
			{
				Separator.Visibility = Visibility.Collapsed;
			}
			else
			{
				Separator.Visibility = Visibility.Visible;
			}
		}

		private void OnBackRequested(object sender, BackRequestedEventArgs e)
		{
			if (MainContainer.CanGoBack)
				return;

			if(StartGrid.Visibility == Visibility.Collapsed)
			{
				ShowStart();
			}

			e.Handled = true;
		}
	}
}
