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

		public DateTimeOffset DateOffset
		{
			get
			{
				return new DateTimeOffset(Date);
			}
			set
			{
				Model.Date = value.Date;
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

		public string CostWithCurrency
		{
			get
			{
				return Model.Cost + MainViewModel.Current.Currency;
			}
		}

		public TagViewModel Tag { get; }

		internal CostModel Model { get; }
	}
}
