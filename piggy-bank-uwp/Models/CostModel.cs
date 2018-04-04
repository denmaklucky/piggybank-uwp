using piggy_bank_uwp.Models;

namespace piggy_bank_uwp.Model
{
    public sealed class CostModel : IBaseModel
    {
        public long Date { get; set; }

        public ulong Cost { get; set; }

        public string Comment { get; set; }

        public string TagId { get; set; }

        public string Id { get; set; }
    }
}
