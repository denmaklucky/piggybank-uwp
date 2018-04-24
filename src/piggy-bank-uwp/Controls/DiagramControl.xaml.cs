using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModels.Diagram;
using Telerik.UI.Xaml.Controls.Chart;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace piggy_bank_uwp.Controls
{
    public sealed partial class DiagramControl : UserControl
    {
        private DiagramViewModel _diagram;

        public DiagramControl()
        {
            this.InitializeComponent();
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            _diagram = args.NewValue as DiagramViewModel;

            Diagram.Series[0].ItemsSource = _diagram.Datas;

            ChartPalette palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in _diagram.Datas)
            {
                palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            }

            Diagram.Palette = palette;
        }
    }
}
