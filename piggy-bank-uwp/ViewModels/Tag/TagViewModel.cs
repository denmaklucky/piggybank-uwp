using piggy_bank_uwp.Model;

namespace piggy_bank_uwp.ViewModel.Tag
{
	public class TagViewModel
	{
		public TagViewModel()
		{

		}

		internal TagViewModel(TagModel model)
		{
			Model = model;
		}

		public string Title
		{
			get
			{
				return Model.Title;
			}
			set
			{
				if(Model.Title != value)
				{
					Model.Title = value;
				}
			}
		}

		public string Color
		{
			get
			{
				return Model.HexColor;
			}
			set
			{
				if(Model.HexColor != value)
				{
					Model.HexColor = value;
				}
			}
		}

		internal TagModel Model { get; }
	}
}
