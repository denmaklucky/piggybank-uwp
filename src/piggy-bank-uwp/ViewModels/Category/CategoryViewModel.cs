using piggy_bank_uwp.Models;
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

        public string Description
        {
            get
            {
                return Model.Description;
            }
            set
            {
                if(Model.Description != value)
                {
                    Model.Description = value;
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

        public bool IsNew { get; set; }

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
