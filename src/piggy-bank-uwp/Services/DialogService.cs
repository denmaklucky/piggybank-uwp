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

        public static ContentDialog GetPurchaseStatusDialog(string status)
        {
            return new ContentDialog
            {
                Content = status,
                PrimaryButtonText = Localize.GetTranslateByKey(Localize.Ok)
            };
        }
    }
}
