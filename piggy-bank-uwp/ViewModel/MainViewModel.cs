using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.ViewModel.Cost;
using piggy_bank_uwp.ViewModel.Tag;
using System.Collections.Generic;
using System.Globalization;

namespace piggy_bank_uwp.ViewModel
{
	public class MainViewModel : BaseViewModel
	{
		private MainViewModel()
		{
			Costs = new List<CostViewModel>();
			Tags = new List<TagViewModel>();
			CurrentBalance = "45000 Р";
			Currency= NumberFormatInfo.CurrentInfo.CurrencySymbol;
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
		}

		internal void DeleteCost(CostViewModel cost)
		{
			Costs.Remove(cost);
			RaisePropertyChanged(nameof(Cost));
		}

		public string CurrentBalance { get; }

		public List<CostViewModel> Costs { get; }

		public List<TagViewModel> Tags { get; }

		public string Currency { get; set; }

		public static MainViewModel Current = new MainViewModel();		
	}
}
