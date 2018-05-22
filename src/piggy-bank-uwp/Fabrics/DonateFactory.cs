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
                Title = "Не плохо",
                Price = "1$"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Хорошо",
                Price = "5$"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Ммм..вкусно",
                Price = "10$"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Велеколепно",
                Price = "100$"
            };

            yield return item;

            item = new DonateItemViewModel
            {
                Title = "Я объелся х_х",
                Price = "1000$"
            };

            yield return item;
        }
    }
}
