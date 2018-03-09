using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.ViewModel.Cost;
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
			foreach (var cost in CostFactory.GetCosts())
			{
				Costs.Add(cost);
			}
		}

		public string CurrentBalance { get; }

		public List<CostViewModel> Costs { get; }
	}
}
