using piggy_bank_uwp.Models;

namespace piggy_bank_uwp.Models
{
    public sealed class CostModel : IBaseModel
    {
        public long Date { get; set; }

        public int Cost { get; set; }

        public string Comment { get; set; }

        public string CategoryId { get; set; }

        public string BalanceId { get; set; }

        public string Id { get; set; }
    }
}
