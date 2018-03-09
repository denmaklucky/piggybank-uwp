using piggy_bank_uwp.Model;
using piggy_bank_uwp.ViewModel.Tag;
using System;

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

		public string Cost
		{
			get
			{
				return $"{Model.Cost} ₽";
			}
			set
			{
				
					Model.Cost = ulong.Parse(value);
			}
		}

		public TagViewModel Tag { get; }

		internal CostModel Model { get; }
	}
}
