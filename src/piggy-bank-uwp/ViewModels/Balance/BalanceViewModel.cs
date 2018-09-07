using piggy_bank_uwp.Models;
using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Interface;
using System;
using System.Globalization;

namespace piggy_bank_uwp.ViewModels.Balance
{
    public class BalanceViewModel : BaseViewModel, IUpdateable
    {
        public BalanceViewModel()
        {
            Model = new BalanceModel
            {
                Id = SystemUtility.GetGuid(),
                Currency = NumberFormatInfo.CurrentInfo.CurrencySymbol
            };

            IsNew = true;
        }

        internal BalanceViewModel(BalanceModel model)
        {
            Model = model;
            IsNew = false;
        }

        public void Update()
        {
            RaisePropertiesChanged();
        }

        internal void ChanngeBalance(int delta)
        {
            Balance += delta;
            RaisePropertyChanged(nameof(CurrentBalance));
        }

        internal void AddCost(int cost)
        {
            Balance -= cost;
        }

        public string Name
        {
            get
            {
                return Model.Name;
            }
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                }
            }
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
                if (Model.Comment != value)
                {
                    Model.Comment = value;
                }
            }
        }

        public bool IsNew { get; set; }

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

        public string Id
        {
            get
            {
                return Model.Id;
            }
        }

        internal BalanceModel Model { get; private set; }
    }
}
