using piggy_bank_uwp.ViewModel;
using System.Globalization;

namespace piggy_bank_uwp.ViewModels.Balance
{
    public class BalanceViewModel : BaseViewModel
    {
        public BalanceViewModel()
        {
            Currency = NumberFormatInfo.CurrentInfo.CurrencySymbol;
        }

        public long Balance { get; private set; }

        public string CurrentBalance
        {
            get
            {
                return Balance + Currency;
            }
        }

        public string Currency { get; private set; }
    }
}
