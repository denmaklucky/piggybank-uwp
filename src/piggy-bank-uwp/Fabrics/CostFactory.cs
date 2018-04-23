using piggy_bank_uwp.Models;
using piggy_bank_uwp.ViewModel.Cost;
using System.Collections.Generic;

namespace piggy_bank_uwp.Fabrics
{
    public static class CostFactory
    {
        public static IEnumerable<CostViewModel> GetCosts()
        {
            CategoryModel tag = new CategoryModel { Title = "Лишние расходы", HexColor = "#F27464" };
            CostModel costModel = new CostModel
            {
                Comment = "Сходили в кино",
            };

            CostViewModel cost = new CostViewModel(costModel);
            yield return cost;

            tag = new CategoryModel { Title = "Продукты. питания", HexColor = "#3C903C" };
            costModel = new CostModel
            {
                Comment = "Купил продукты в Спаре",
                Cost = 2998,
            };

            cost = new CostViewModel(costModel);
            yield return cost;

            tag = new CategoryModel { Title = "Без категории", HexColor = "#241B9D" };
            costModel = new CostModel
            {
                Comment = "Проезд до Глаба Успенского",
                Cost = 56,
            };

            cost = new CostViewModel(costModel);
            yield return cost;

            tag = new CategoryModel { Title = "тусэ", HexColor = "#EC6618" };
            costModel = new CostModel
            {
                Comment = "Сходили в клуб на Грот",
                Cost = 5000,
            };

            cost = new CostViewModel(costModel);
            yield return cost;

            tag = new CategoryModel { Title = "Квартира", HexColor = "#8C409C" };
            costModel = new CostModel
            {
                Comment = "Оплатил комунальные услуги",
                Cost = 5000,
            };

            cost = new CostViewModel(costModel);
            yield return cost;

            tag = new CategoryModel { Title = "Животные", HexColor = "#0A29F5" };
            costModel = new CostModel
            {
                Comment = "Купил Софии корм",
                Cost = 3267,
            };

            cost = new CostViewModel(costModel);
            yield return cost;

            tag = new CategoryModel { Title = "Продукты. питания", HexColor = "#3C903C" };
            costModel = new CostModel
            {
                Comment = "Закупили продукты на недели в Ленте",
                Cost = 6327,
            };

            cost = new CostViewModel(costModel);
            yield return cost;
        }
    }
}
