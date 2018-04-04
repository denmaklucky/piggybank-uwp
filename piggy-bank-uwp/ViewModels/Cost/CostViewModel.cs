using piggy_bank_uwp.Model;
using piggy_bank_uwp.ViewModel.Tag;

namespace piggy_bank_uwp.ViewModel.Cost
{
    public class CostViewModel : BaseViewModel
    {
        public CostViewModel()
        {

        }

        internal CostViewModel(CostModel model)
            : this()
        {
            Model = model;
        }

        public string Comment
        {
            get
            {
                return Model.Comment;
            }
            set
            {
                if (Model.Comment != value)
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
                if (Model.Cost != value)
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
