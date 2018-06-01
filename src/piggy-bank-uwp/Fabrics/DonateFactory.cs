using piggy_bank_uwp.ViewModels.Donate;
using System.Collections.Generic;

namespace piggy_bank_uwp.Fabrics
{
    public static class DonateFactory
    {
        public static IEnumerable<DonateItemViewModel> GetItems()
        {
            DonateItemViewModel item = new DonateItemViewModel
            {
                Title = "Not bad!",
                Price = "1$",
                StoreId = "piggy_one"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Good!",
                Price = "2$",
                StoreId = "piggy_two"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Very good!",
                Price = "3$",
                StoreId = "piggy_three"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Excellently!",
                Price = "4$",
                StoreId = "piggy_four"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "OMG!",
                Price = "5$",
                StoreId = "piggy_five"

            };

            yield return item;
        }
    }
}
