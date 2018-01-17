using piggy_bank_uwp.View.Costs;
using piggy_bank_uwp.ViewModel;
using Windows.UI;
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
			//check that acrylic is avaliable
			if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
			{
				Windows.UI.Xaml.Media.AcrylicBrush myBrush = new Windows.UI.Xaml.Media.AcrylicBrush();
				myBrush.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
				myBrush.TintColor = Color.FromArgb(255, 202, 24, 37);
				myBrush.FallbackColor = Color.FromArgb(255, 202, 24, 37);
				myBrush.TintOpacity = 0.3;
				MainRelativePanel.Background = myBrush;
			}
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

		private void RelativePanelSizeChanged(object sender, SizeChangedEventArgs e)
		{
			ButtonCommandBar.Width = e.NewSize.Width;
			CostsListView.Width = e.NewSize.Width;
		}

	}
}
