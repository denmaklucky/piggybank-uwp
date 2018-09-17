using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Interface;
using piggy_bank_uwp.Workers;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace piggy_bank_uwp.ViewModels.Balance
{
    public class AccountsViewModel : BaseViewModel, IBaseViewModel
    {
        private DbWorker _dbWorker;
        private string _currency;

        public AccountsViewModel()
        {
            _dbWorker = DbWorker.Current;
            Balances = new ObservableCollection<BalanceViewModel>();
            _currency = NumberFormatInfo.CurrentInfo.CurrencySymbol;
        }

        public void Initialization()
        {
            foreach (var balance in _dbWorker.GetBalances())
            {
                Balances.Add(new BalanceViewModel(balance));
            }
        }

        public void Finalization()
        {
            throw new NotImplementedException();
        }

        internal void UpdateData()
        {
            Balances.Clear();
            foreach (var balance in _dbWorker.GetBalances())
            {
                Balances.Add(new BalanceViewModel(balance));
            }
        }

        public void RaiseBalance()
        {
            RaisePropertyChanged(nameof(Balances));
            RaisePropertyChanged(nameof(TotalBalance));
        }

        public string TotalBalance
        {
            get
            {
                int totalBalance = Balances.Sum(b => b.Balance);

                return $"{totalBalance} {_currency}";
            }
        }

        public ObservableCollection<BalanceViewModel> Balances { get; }
    }
}
