using piggy_bank_uwp.Model;
using piggy_bank_uwp.ViewModel.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Fabrics
{
	public static class TagFactory
	{
		public static IEnumerable<TagViewModel> GetTags()
		{
			TagModel model = new TagModel { Title = "Лишние расходы", HexColor = "#F27464" };
			TagViewModel tag = new TagViewModel(model);
			yield return tag;

			model = new TagModel { Title = "Продукты. питания", HexColor = "#3C903C" };
			tag = new TagViewModel(model);
			yield return tag;

			model = new TagModel { Title = "Без категории", HexColor = "#241B9D" };
			tag = new TagViewModel(model);
			yield return tag;

			model = new TagModel { Title = "тусэ", HexColor = "#EC6618" };
			tag = new TagViewModel(model);
			yield return tag;

			model = new TagModel { Title = "Квартира", HexColor = "#8C409C" };
			tag = new TagViewModel(model);
			yield return tag;

			model = new TagModel { Title = "Животные", HexColor = "#0A29F5" };
			tag = new TagViewModel(model);
			yield return tag;
		}
	}
}
