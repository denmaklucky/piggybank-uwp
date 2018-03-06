using piggy_bank_uwp.Model;
using piggy_bank_uwp.ViewModel.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piggy_bank_uwp.ViewModel.Cost
{
	public class CostViewModel : BaseViewModel
	{
		public CostViewModel()
		{

		}

		internal CostViewModel(CostModel model)
			:this()
		{
			Model = model;
			Tag = new TagViewModel(Model.Tag);
		}

		public DateTime Date
		{
			get
			{
				return Model.Date;
			}
			set
			{
				if(Model.Date != value)
				{
					Model.Date = value;
				}
			}
		}

		public string Comment
		{
			get
			{
				return Model.Comment;
			}
			set
			{
				if(Model.Comment != value)
				{
					Model.Comment = value;
				}
			}
		}

		public ulong Cost
		{
			get
			{
				return Model.Cost;
			}
			set
			{
				if(Model.Cost != value)
				{
					Model.Cost = value;
				}
			}
		}

		public TagViewModel Tag { get; }

		internal CostModel Model { get; }
	}
}
