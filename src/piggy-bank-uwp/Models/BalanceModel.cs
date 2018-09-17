namespace piggy_bank_uwp.Models
{
    public class BalanceModel : IBaseModel
    {
        public int Balance { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}
