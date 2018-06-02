using Windows.UI.Xaml.Controls;

namespace piggy_bank_uwp.Services
{
    public static class DialogService
    {
        public static ContentDialog GetInformationDialog(string content)
        {
            return new ContentDialog
            {
                Content = content,
                PrimaryButtonText = Localize.GetTranslateByKey(Localize.Ok)
            };
        }
    }
}
