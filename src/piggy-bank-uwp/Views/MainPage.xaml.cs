using piggy_bank_uwp.View.Diagram;
using piggy_bank_uwp.View.Donate;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.Views.Categories;
using piggy_bank_uwp.Views.Costs;
using piggy_bank_uwp.Views.SettingsPage;
using piggy_bank_uwp.Views.Sync;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;

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
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();

                e.Handled = true;
            }
        }

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                NavView.Header = "Settings";
            }
            else
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavViewNavigate(item as NavigationViewItem);
            }
        }

        private void NavViewNavigate(NavigationViewItem navigationViewItem)
        {
            switch (navigationViewItem.Tag)
            {
                case Constants.costs:
                    ContentFrame.Navigate(typeof(CostsPage));
                    NavView.Header = "Costs";
                    break;
                case Constants.categories:
                    ContentFrame.Navigate(typeof(CategoriesPage));
                    NavView.Header = "Categories";
                    break;
                case Constants.diagrams:
                    ContentFrame.Navigate(typeof(DiagramPage), _mainViewModel.Diagram);
                    NavView.Header = "Diagrams";
                    break;
                case Constants.synchronization:
                    ContentFrame.Navigate(typeof(SyncPage));
                    NavView.Header = "Synchronization";
                    break;
                case Constants.donate:
                    ContentFrame.Navigate(typeof(DonatePage), _mainViewModel.Donate);
                    NavView.Header = "Donate";
                    break;
            }
        }

        private void OnNavViewLoaded(object sender, RoutedEventArgs e)
        {
            var item = NavView.MenuItems.OfType<NavigationViewItem>().First();
            NavViewNavigate(item as NavigationViewItem);
        }

        private async void OnFeedbackTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
        }
    }
}
