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
            Accounts = new AccountsViewModel();
            Diagram = new DiagramViewModel();
            OneDrive = new OneDriveViewModel();
            Donate = new DonateViewModel();
        }

        public void Init()
        {
            IsInit = false;

            List<CategoryModel> categories = null;

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

            Accounts.Initialization();

            IsInit = true;
        }

        public void Finit()
        {
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

        public void UpdateData()
        {
            if (!OneDrive.IsAuthenticated)
                return;

            List<CategoryModel> categories = DbWorker.GetCategories();

            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            Costs.Clear();
            foreach (var cost in DbWorker.GetCosts().Take(TOTAL_COUNT_COSTS))
            {
                Costs.Add(new CostViewModel(cost));
            }

            Accounts.UpdateData();
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

                BalanceViewModel currentBalance = Accounts.Balances.FirstOrDefault(b => b.Id == newCost.BalanceId);

                if (currentBalance != null)
                {
                    currentBalance.AddCost(newCost.Cost);
                    DbWorker.UpdateBalance(currentBalance.Model);
                }

                DbWorker.AddCost(newCost.Model);

                Accounts.RaiseBalance();
            });
        }

        internal Task UpdateCost(CostViewModel updateCost)
        {
            return Task.Factory.StartNew(() =>
            {
                updateCost.Update();

                if (updateCost.HavePrevCost)
                {
                    //TODO: o(n) - bad
                    BalanceViewModel currentBalance = Accounts.Balances.FirstOrDefault(b=> b.Id == updateCost.BalanceId);

                    if(currentBalance != null)
                    {
                        currentBalance.ChanngeBalance(DbWorker.GetCost(updateCost.Id).Cost);
                        currentBalance.AddCost(updateCost.Cost);
                        DbWorker.UpdateBalance(currentBalance.Model);
                    }
                    updateCost.HavePrevCost = false;
                }

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

        #region Balances

        internal Task AddBalance(BalanceViewModel newBalance)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Accounts.Balances.Add(newBalance);
                });

                DbWorker.AddBalance(newBalance.Model);
                Accounts.RaiseBalance();
            });
        }

        internal Task UpdateBalance(BalanceViewModel updateBalance)
        {
            return Task.Factory.StartNew(() =>
            {
                updateBalance.Update();
                DbWorker.UpdateBalance(updateBalance.Model);
            });
        }

        internal Task DeleteBalance(BalanceViewModel balance)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Accounts.Balances.Remove(balance);
                });

                DbWorker.RemoveBalance(balance.Model);
                Accounts.RaiseBalance();
            });
        }

        #endregion

        public bool IsInit { get; private set; }

        public ObservableCollection<CostViewModel> Costs { get; }

        public ObservableCollection<CategoryViewModel> Categories { get; }

        public AccountsViewModel Accounts { get; }

        public OneDriveViewModel OneDrive { get; }

        public DiagramViewModel Diagram { get; }

        public DonateViewModel Donate { get; }

        public DbWorker DbWorker { get; }

        public bool CanShowToast => SettingsWorker.Current.GetNotificatinsSetting() && ((DateTime.UtcNow - SettingsWorker.Current.GetLastTimeShow())?.Days ?? DAY_REMINDER - 1) > DAY_REMINDER;

        public bool HaveCategories => Categories.GetEnumerator().MoveNext();

        public static MainViewModel Current = new MainViewModel();
    }
}
