using piggy_bank_uwp.ViewModels.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Services.Store;
using System;
using piggy_bank_uwp.Utilities;

namespace piggy_bank_uwp.ViewModels.Donate
{
    public class DonateViewModel : IBaseViewModel
    {
        private StoreContext _storeContext;
        private bool _isLoaded;
        public DonateViewModel()
        {
            Items = new List<DonateItemViewModel>();
            _storeContext = StoreContext.GetDefault();
        }

        public async Task InitializationAsyn()
        {
            if (_isLoaded)
                return;

            string[] productKinds = { "UnmanagedConsumable" };
            var queryResult = await _storeContext.GetAssociatedStoreProductsAsync(productKinds);

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                DonateItemViewModel donateItem = new DonateItemViewModel
                {
                    Title = DonateUtility.GetValidurchaseName(item.Value.InAppOfferToken),
                    StoreId = item.Key,
                    Price = item.Value.Price.FormattedPrice
                };
                Items.Add(donateItem);
            }

            _isLoaded = true;
        }

        public void Initialization()
        {
        }

        public void Finalization()
        {
        }

        public async Task<string> BuyItem(DonateItemViewModel item)
        {
            try
            {
                var result = await _storeContext.RequestPurchaseAsync(item?.StoreId);
                switch (result.Status)
                {
                    case StorePurchaseStatus.Succeeded:
                        return Localize.GetTranslateByKey(Localize.PurchaseStatusOk);
                    case StorePurchaseStatus.NotPurchased:
                        return String.Empty;
                    default:
                        return Localize.GetTranslateByKey(Localize.PurchaseStatusBad);
                }
            }
            catch
            {
                return Localize.GetTranslateByKey(Localize.PurchaseStatusBad);
            }
        }

        public List<DonateItemViewModel> Items { get; }
    }
}
