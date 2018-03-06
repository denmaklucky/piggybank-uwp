using piggy_bank_uwp.Model;
using piggy_bank_uwp.ViewModel.Cost;
using System;
using System.Collections.Generic;

namespace piggy_bank_uwp.ViewModel
{
	public class MainViewModel : BaseViewModel
	{
		public MainViewModel()
		{
			Costs = new List<CostViewModel>();
			CurrentBalance = "45000 Р";
		}

		public void Init()
		{
			TagModel tag = new TagModel { Title = "тусэ", HexColor = "#F27464" };
			CostModel costModel = new CostModel { Comment = "Сходили в кино", Cost = 1000, Date = DateTime.Now, Tag = tag };

			CostViewModel cost = new CostViewModel(costModel);

			Costs.Add(cost);
		}

		public string CurrentBalance { get; }

		public List<CostViewModel> Costs { get; }
	}
}
