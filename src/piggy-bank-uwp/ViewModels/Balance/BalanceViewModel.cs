using piggy_bank_uwp.Models;
using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Interface;
using piggy_bank_uwp.Workers;
using System.Globalization;

namespace piggy_bank_uwp.ViewModels.Balance
{
    public class BalanceViewModel : BaseViewModel, IBaseViewModel
    {
        public void Initialization()
        {
            BalanceModel balance = DbWorker.Current.GetBalance();

            if (balance == null)
            {
                Model = new BalanceModel
                {
                    Balance = 0,
                    Currency = NumberFormatInfo.CurrentInfo.CurrencySymbol,
                    Id = SystemUtility.GetGuid()
                };

                DbWorker.Current.AddBalance(Model);
            }
            else
            {
                Model = balance;
            }
        }

        public void Finalization()
        {
            DbWorker.Current.UpdateBalance(Model);
        }

        internal void ChanngeBalance(int delta)
        {
            Balance += delta;

            Finalization();
            RaisePropertyChanged(nameof(CurrentBalance));
        }

        internal void AddCost(int cost)
        {
            Balance -= cost;

            Finalization();
        }

        public int Balance
        {
            get
            {
                return Model.Balance;
            }
            set
            {
                if (Model.Balance != value)
                {
                    Model.Balance = value;
                }
            }
        }

        public string Comment
        {
            get
            {
                return Model.Comment;
            }
            set
            {
                if(Model.Comment != value)
                {
                    Model.Comment = value;
                }
            }
        }

        public string CurrentBalance
        {
            get
            {
                return $"{Balance} {Currency}";
            }
        }

        public string Currency
        {
            get
            {
                return Model.Currency;
            }
        }

        internal BalanceModel Model { get; private set; }

    }
}
