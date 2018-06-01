using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.ViewModels.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Services.Store;
using System;

namespace piggy_bank_uwp.ViewModels.Donate
{
    public class DonateViewModel : IBaseViewModel
    {
        private StoreContext storeContext;
        public DonateViewModel()
        {
            Items = new List<DonateItemViewModel>();
            storeContext = StoreContext.GetDefault();
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
        }

        public async Task BuyItem(DonateItemViewModel item)
        {
            try
            {
                var result = await storeContext.RequestPurchaseAsync(item?.StoreId);

                switch (result.Status)
                {
                    case StorePurchaseStatus.Succeeded:
                        break;
                    case StorePurchaseStatus.AlreadyPurchased:
                        break;
                    case StorePurchaseStatus.NotPurchased:
                        break;
                    case StorePurchaseStatus.NetworkError:
                        break;
                    case StorePurchaseStatus.ServerError:
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }

        public List<DonateItemViewModel> Items { get; }
    }
}
