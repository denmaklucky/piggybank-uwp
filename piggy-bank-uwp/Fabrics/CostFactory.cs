using piggy_bank_uwp.Model;
using piggy_bank_uwp.ViewModel.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Fabrics
{
	public static class CostFactory
	{
		public static IEnumerable<CostViewModel> GetCosts()
		{
			TagModel tag = new TagModel { Title = "Лишние расходы", HexColor = "#F27464" };
			CostModel costModel = new CostModel
			{
				Comment = "Сходили в кино",
				Cost = 1000, Date = DateTime.Now,
				Tag = tag
			};

			CostViewModel cost = new CostViewModel(costModel);
			yield return cost;

			tag = new TagModel { Title = "Продукты. питания", HexColor = "#3C903C" };
			costModel = new CostModel
			{
				Comment = "Купил продукты в Спаре",
				Cost = 2998,
				Date = DateTime.Now,
				Tag = tag
			};

			cost = new CostViewModel(costModel);
			yield return cost;

			tag = new TagModel { Title = "Без категории", HexColor = "#241B9D" };
			costModel = new CostModel
			{
				Comment = "Проезд до Глаба Успенского",
				Cost = 56,
				Date = DateTime.Now,
				Tag = tag
			};

			cost = new CostViewModel(costModel);
			yield return cost;

			tag = new TagModel { Title = "тусэ", HexColor = "#EC6618" };
			costModel = new CostModel
			{
				Comment = "Сходили в клуб на Грот",
				Cost = 5000,
				Date = DateTime.Now,
				Tag = tag
			};

			cost = new CostViewModel(costModel);
			yield return cost;

			tag = new TagModel { Title = "Квартира", HexColor = "#8C409C" };
			costModel = new CostModel
			{
				Comment = "Оплатил комунальные услуги",
				Cost = 5000,
				Date = DateTime.Now,
				Tag = tag
			};

			cost = new CostViewModel(costModel);
			yield return cost;

			tag = new TagModel { Title = "Животные", HexColor = "#0A29F5" };
			costModel = new CostModel
			{
				Comment = "Купил Софии корм",
				Cost = 3267,
				Date = DateTime.Now,
				Tag = tag
			};

			cost = new CostViewModel(costModel);
			yield return cost;

			tag = new TagModel { Title = "Продукты. питания", HexColor = "#3C903C" };
			costModel = new CostModel
			{
				Comment = "Закупили продукты на недели в Ленте",
				Cost = 6327,
				Date = DateTime.Now,
				Tag = tag
			};

			cost = new CostViewModel(costModel);
			yield return cost;
		}
	}
}
