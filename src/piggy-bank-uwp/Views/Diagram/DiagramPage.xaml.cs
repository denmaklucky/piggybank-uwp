using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModels.Diagram;
using Telerik.UI.Xaml.Controls.Chart;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace piggy_bank_uwp.View.Diagram
{
    public sealed partial class DiagramPage : Page
    {
        private DiagramViewModel _diagram;

        public DiagramPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _diagram = e.Parameter as DiagramViewModel;

            _diagram.Initialization();

            Diagram.Series[0].ItemsSource = _diagram.Datas;

            ChartPalette palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in _diagram.Datas)
            {
                palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            }

            Diagram.Palette = palette;
            LabelsListView.ItemsSource = _diagram.Datas;

            if (!_diagram.IsEmpty)
                _diagram.UpdateTile();
        }

        private void OnFilterClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FilterFlyout.ShowAt((AppBarButton)sender);
        }

        private void OnCheckMarkClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FilterFlyout.Hide();

            _diagram.ApplyFilter(StartDatePicker.Date.Date, EndDatePicker.Date.Date);

            Diagram.Series[0].ItemsSource = null;
            Diagram.Series[0].ItemsSource = _diagram.Datas;

            ChartPalette palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in _diagram.Datas)
            {
                palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            }

            Diagram.Palette = palette;
            LabelsListView.ItemsSource = null;
            LabelsListView.ItemsSource = _diagram.Datas;
        }

        private void OnCancelClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FilterFlyout.Hide();
        }
    }
}
