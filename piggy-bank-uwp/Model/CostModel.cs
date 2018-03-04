using System;

namespace piggy_bank_uwp.Model
{
	public sealed class CostModel
	{
		public DateTime Date { get; set; }
		
		public ulong Cost { get; set; }

		public string Comment { get; set; }

		public TagModel Tag { get; set; }
	}
}
