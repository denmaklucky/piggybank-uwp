using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace piggy_bank_uwp.ViewModels.Donate
{
    public class DonateViewModel : IBaseViewModel
    {
        public DonateViewModel()
        {
            Items = new List<DonateItemViewModel>();
        }

        public void Initialization()
        {
            foreach (var item in DonateFactory.GetItems())
            {
                Items.Add(item);
            }
        }

        public void Finalization()
        {
            throw new NotImplementedException();
        }

        public async Task BuyItem(DonateItemViewModel item)
        {

        }

        public List<DonateItemViewModel> Items { get; }
    }
}
