using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.ViewModel.Cost;
using piggy_bank_uwp.ViewModel.Tag;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace piggy_bank_uwp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private MainViewModel()
        {
            Costs = new ObservableCollection<CostViewModel>();
            Tags = new List<TagViewModel>();
            CurrentBalance = "45000 Р";
            Currency = NumberFormatInfo.CurrentInfo.CurrencySymbol;
        }

        public void Init()
        {
            foreach (var cost in CostFactory.GetCosts())
            {
                Costs.Add(cost);
            }

            foreach (var tag in TagFactory.GetTags())
            {
                Tags.Add(tag);
            }

            RaisePropertyChanged(nameof(Cost));
        }

        internal Task DeleteCost(CostViewModel cost)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Costs.Remove(cost);
                });

                RaisePropertyChanged(nameof(Costs));
            });
        }

        public string CurrentBalance { get; private set; }

        public ObservableCollection<CostViewModel> Costs { get; }

        public List<TagViewModel> Tags { get; }

        public string Currency { get; set; }

        public static MainViewModel Current = new MainViewModel();
    }
}
