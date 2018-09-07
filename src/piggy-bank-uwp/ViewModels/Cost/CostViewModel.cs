using piggy_bank_uwp.Models;
using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModel.Tag;
using piggy_bank_uwp.ViewModels.Interface;
using System;
using System.Linq;

namespace piggy_bank_uwp.ViewModel.Cost
{
    public class CostViewModel : BaseViewModel, IUpdateable
    {
        public CostViewModel()
        {
            Model = new CostModel { Id = SystemUtility.GetGuid() };
            IsNew = true;
            HavePrevCost = false;
        }

        internal CostViewModel(CostModel model)
        {
            Model = model;
            IsNew = false;
            HavePrevCost = false;
        }

        public void Update()
        {
            RaisePropertiesChanged();
        }

        public void ChangedCategory(string categoryId)
        {
            if (String.IsNullOrEmpty(categoryId))
                return;

            Model.CategoryId = categoryId;
        }

        public void ChangedBalance(string balanceId)
        {
            if (String.IsNullOrEmpty(balanceId))
                return;

            Model.BalanceId = balanceId;
        }

        public bool IsNew { get; set; }

        public bool HavePrevCost { get; set; }

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

        public int Cost
        {
            get
            {
                return Model.Cost;
            }
            set
            {
                if (Model.Cost != value)
                {
                    if (!IsNew)
                        HavePrevCost = true;

                    Model.Cost = value;
                }
            }
        }

        public DateTimeOffset DateOffset
        {
            get
            {
                if (Model.Date <= 0)
                    return DateTimeOffset.UtcNow;

                var localDate = DateUtility.GetLocalTimeFromUTCMilliseconds(Model.Date);
                return DateTimeOffset.Parse(localDate.ToString());
            }
            set
            {
                Model.Date = DateUtility.GetUTCMillisecondsFromDateTime(value.UtcDateTime);
            }
        }

        public string Date
        {
            get
            {
                string format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                return DateUtility.GetLocalTimeFromUTCMilliseconds(Model.Date).Date.ToString(format);
            }
        }

        public string CostWithCurrency
        {
            get
            {
                return Model.Cost + MainViewModel.Current.Accounts.Balances.FirstOrDefault(b => b.Id == BalanceId)?.Currency;
            }
        }

        public string Id
        {
            get
            {
                return Model.Id;
            }
        }

        public string CategoryId
        {
            get
            {
                return Model.CategoryId;
            }
        }

        public string BalanceId
        {
            get
            {
                return Model.BalanceId;
            }
        }

        public CategoryViewModel Category
        {
            get
            {
                return MainViewModel.Current.Categories.FirstOrDefault(c => c.Id == CategoryId);
            }
        }

        internal CostModel Model { get; }
    }
}
