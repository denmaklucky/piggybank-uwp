using Windows.UI.Xaml.Media;

namespace piggy_bank_uwp.ExtensionMethods
{
    public static class SolidColorBrushExtension
    {
        public static string ToColor(this SolidColorBrush brush)
        {
            return brush.Color.ToString();
        }
    }
}
