using piggy_bank_uwp.Model;
using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModels.Interface;

namespace piggy_bank_uwp.ViewModel.Tag
{
    public class CategoryViewModel : BaseViewModel, IUpdateable
    {
        public CategoryViewModel()
        {
            Model = new CategoryModel { Id = SystemUtility.GetGuid() };
            IsNew = true;
        }

        internal CategoryViewModel(CategoryModel model)
        {
            Model = model;
            IsNew = false;
        }

        public void Update()
        {
            RaisePropertiesChanged();
        }

        public string Title
        {
            get
            {
                return Model.Title;
            }
            set
            {
                if (Model.Title != value)
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
                if (Model.HexColor != value)
                {
                    Model.HexColor = value;
                }
            }
        }

        public bool IsNew { get; private set; }

        public string Id
        {
            get
            {
                return Model.Id;
            }
        }

        internal CategoryModel Model { get; }
    }
}
