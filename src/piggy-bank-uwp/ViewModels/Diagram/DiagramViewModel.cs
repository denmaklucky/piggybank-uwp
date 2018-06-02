using Microsoft.Toolkit.Uwp.Notifications;
using piggy_bank_uwp.Models;
using piggy_bank_uwp.Services;
using piggy_bank_uwp.Utilities;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Interface;
using piggy_bank_uwp.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace piggy_bank_uwp.ViewModels.Diagram
{
    public class DiagramViewModel : BaseViewModel, IBaseViewModel
    {
        public DiagramViewModel()
        {
            Datas = new List<DataDiagramViewModel>();
        }

        public void Initialization()
        {
            List<CostModel> costs = DbWorker.Current.GetCosts();
            AllCosts = costs.Sum(c => c.Cost);

            Datas.Clear();

            foreach (var category in MainViewModel.Current.Categories)
            {
                var costsByCategory = costs.Where(c => c.CategoryId == category.Id);

                if (costsByCategory.Count() > 0)
                {
                    double sumInCategory = costsByCategory.Sum(c => c.Cost);
                    Datas.Add(
                        new DataDiagramViewModel
                        {
                            Value = (sumInCategory / AllCosts) * 100,
                            Color = category.Color,
                            Title = category.Title
                        });
                }
            }
        }

        public void Finalization()
        {

        }

        public void ApplyFilter(DateTime startDate, DateTime endDate)
        {
            var costs = DbWorker.Current.GetCosts().Where(c => DateUtility.GetLocalTimeFromUTCMilliseconds(c.Date) > startDate && DateUtility.GetLocalTimeFromUTCMilliseconds(c.Date) < endDate);

            Datas.Clear();

            foreach (var category in MainViewModel.Current.Categories)
            {
                var tempCosts = costs.Where(c => c.CategoryId == category.Id);

                if (tempCosts.Count() > 0)
                {
                    double sumInCategory = tempCosts.Sum(c => c.Cost);
                    Datas.Add(
                        new DataDiagramViewModel
                        {
                            Value = (sumInCategory / AllCosts) * 100,
                            Color = category.Color,
                            Title = category.Title
                        });
                }
            }
        }

        public Task UpdateTile()
        {
            return Task.Factory.StartNew(() =>
            {
                if (Datas.Count == 0)
                    return;

                TileContent content = TileService.GenerateTileContent(Datas);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(content.GetXml()));
            });
        }

        public double AllCosts { get; private set; }

        public List<DataDiagramViewModel> Datas { get; }

        public bool IsEmpty
        {
            get
            {
                return Datas.Count == 0;
            }
        }
    }
}
