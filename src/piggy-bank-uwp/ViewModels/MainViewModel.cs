using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.Models;
using piggy_bank_uwp.ViewModel.Cost;
using piggy_bank_uwp.ViewModel.Tag;
using piggy_bank_uwp.ViewModels.Balance;
using piggy_bank_uwp.ViewModels.Diagram;
using piggy_bank_uwp.Workers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace piggy_bank_uwp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private MainViewModel()
        {
            Costs = new ObservableCollection<CostViewModel>();
            Categories = new ObservableCollection<CategoryViewModel>();
            DbWorker = DbWorker.Current;
            Balance = new BalanceViewModel();
            Diagram = new DiagramViewModel();
        }

        public void Init()
        {
            IsInit = false;

            List<CategoryModel> categories = null;

            //TOD: check other method a count 
            if (DbWorker.GetCategories().Count == 0)
            {
                categories = CategoryFactory.GetCategories().ToList();
                foreach (var category in categories)
                {
                    DbWorker.AddCategory(category);
                }
            }
            else
            {
                categories = DbWorker.GetCategories();
            }

            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            foreach (var cost in DbWorker.GetCosts())
            {
                Costs.Add(new CostViewModel(cost));
            }

            Balance.Initialization();
            Diagram.Initialization();

            IsInit = true;
        }

        public void Finit()
        {
            Balance.Finalization();
        }

        internal void UpdateData()
        {
            RaisePropertyChanged(nameof(Balance));
        }

        #region Costs

        internal Task AddCost(CostViewModel newCost)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Costs.Insert(0, newCost);
                });

                Balance.AddCost(newCost.Cost);
                DbWorker.AddCost(newCost.Model);

                RaisePropertyChanged(nameof(Balance));
            });
        }

        internal Task UpdateCost(CostViewModel updateCost)
        {
            return Task.Factory.StartNew(() =>
            {
                updateCost.Update();
                DbWorker.UpdateCost(updateCost.Model);
            });
        }

        internal Task DeleteCost(CostViewModel cost)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Costs.Remove(cost);
                });

                DbWorker.RemoveCost(cost.Model);
            });
        }
        #endregion

        #region Category

        internal Task AddCategory(CategoryViewModel newCategory)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Categories.Add(newCategory);
                });

                DbWorker.AddCategory(newCategory.Model);
            });
        }

        internal Task UpdateCategory(CategoryViewModel updateTag)
        {
            return Task.Factory.StartNew(() =>
            {
                updateTag.Update();
                DbWorker.UpdateCategory(updateTag.Model);
            });
        }

        internal Task DeleteCategory(CategoryViewModel category)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Categories.Remove(category);
                });

                DbWorker.RemoveCategory(category.Model);
            });
        }

        #endregion

        public bool IsInit { get; private set; }

        public ObservableCollection<CostViewModel> Costs { get; }

        public ObservableCollection<CategoryViewModel> Categories { get; }

        public BalanceViewModel Balance { get; }

        public DiagramViewModel Diagram { get; set; }

        public DbWorker DbWorker { get; }

        public static MainViewModel Current = new MainViewModel();
    }
}
