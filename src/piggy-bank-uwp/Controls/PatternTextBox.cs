using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace piggy_bank_uwp.Controls
{
    public sealed class PatternTextBox : TextBox
    {
        public string Pattern
        {
            get {return  (string)GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        public static readonly DependencyProperty PatternProperty = DependencyProperty.Register(nameof(Pattern), typeof(string), typeof(PatternTextBox),
            new PropertyMetadata(default(string)));

        public PatternTextBox()
        {
            TextChanging += OnTextChanging;
        }

        private void OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            StringBuilder buffer = new StringBuilder();

            foreach (var c in Text)
            {
                if (Pattern.Contains(c.ToString()))
                {
                    buffer.Append(c.ToString());
                }
            }

            Text = buffer.ToString();
            SelectionStart = buffer.Length;
        }
    }
}
