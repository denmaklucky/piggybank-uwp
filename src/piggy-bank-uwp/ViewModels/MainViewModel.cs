using Microsoft.Toolkit.Uwp.Notifications;
using piggy_bank_uwp.Fabrics;
using piggy_bank_uwp.Models;
using piggy_bank_uwp.Services;
using piggy_bank_uwp.ViewModel.Cost;
using piggy_bank_uwp.ViewModel.Tag;
using piggy_bank_uwp.ViewModels.Balance;
using piggy_bank_uwp.ViewModels.Diagram;
using piggy_bank_uwp.ViewModels.Donate;
using piggy_bank_uwp.ViewModels.Interface;
using piggy_bank_uwp.ViewModels.Services;
using piggy_bank_uwp.Workers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace piggy_bank_uwp.ViewModel
{
    public class MainViewModel : BaseViewModel, IToastViewModel
    {
        private const int TOTAL_COUNT_COSTS = 10;
        private const int DAY_REMINDER = 5;
        private MainViewModel()
        {
            Costs = new ObservableCollection<CostViewModel>();
            Categories = new ObservableCollection<CategoryViewModel>();
            DbWorker = DbWorker.Current;
            Balance = new BalanceViewModel();
            Diagram = new DiagramViewModel();
            OneDrive = new OneDriveViewModel();
            Donate = new DonateViewModel();
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

            foreach (var cost in DbWorker.GetCosts().Take(TOTAL_COUNT_COSTS))
            {
                Costs.Add(new CostViewModel(cost));
            }

            Balance.Initialization();
            Donate.Initialization();

            IsInit = true;
        }

        public void Finit()
        {
            Balance.Finalization();
        }

        public void ShowToast()
        {
            ToastContent content = ToastService.GenerateToastContent();
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }

        public void SaveLastTimeShow()
        {
            SettingsWorker.Current.SaveLastTimeShow(DateTime.UtcNow);
        }

        internal void UpdateData()
        {
            RaisePropertyChanged(nameof(Balance));
        }

        internal async Task FetchCosts()
        {
            foreach (CostModel item in DbWorker.Current.GetCosts(Costs.Count))
            {
                await Task.Delay(600);

                await App.RunUIAsync(() =>
                {
                    Costs.Add(new CostViewModel(item));
                });
            }
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

        public OneDriveViewModel OneDrive { get; }

        public DiagramViewModel Diagram { get; }

        public DonateViewModel Donate { get; }

        public DbWorker DbWorker { get; }

        public bool CanShowToast => SettingsWorker.Current.GetNotificatinsSetting() && ((DateTime.UtcNow - SettingsWorker.Current.GetLastTimeShow())?.Days ?? DAY_REMINDER+1)> DAY_REMINDER;

        public static MainViewModel Current = new MainViewModel();
    }
}
