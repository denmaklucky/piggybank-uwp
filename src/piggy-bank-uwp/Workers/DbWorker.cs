using piggy_bank_uwp.Models;
using System.Collections.Generic;
using System.Linq;

namespace piggy_bank_uwp.Workers
{
    public sealed class DbWorker
    {
        private DbWorker() { }

        public void AddCost(CostModel cost)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Costs.Add(cost);
                dbContext.SaveChanges();
            }
        }

        public void AddCategory(CategoryModel category)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }
        }

        public void AddBalance(BalanceModel balance)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                if (dbContext.Balance.Count() >= 1)
                {
                    dbContext.Balance.RemoveRange(dbContext.Balance);
                    dbContext.SaveChanges();
                }

                dbContext.Balance.Add(balance);
                dbContext.SaveChanges();
            }
        }

        public void RemoveCost(CostModel cost)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Costs.Remove(cost);
                dbContext.SaveChanges();
            }
        }

        public void RemoveCategory(CategoryModel tag)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Categories.Remove(tag);
                dbContext.SaveChanges();
            }
        }

        public void Clear()
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                //TODO
            }
        }

        public List<CostModel> GetCosts()
        {
            List<CostModel> costs = null;

            using (Context.AppContext dbContext = new Context.AppContext())
            {
                costs = new List<CostModel>(dbContext.Costs);
            }

            return costs;
        }

        public List<CategoryModel> GetCategories()
        {
            List<CategoryModel> categories = null;

            using (Context.AppContext dbContext = new Context.AppContext())
            {
                categories = new List<CategoryModel>(dbContext.Categories);
            }

            return categories;
        }

        public BalanceModel GetBalance()
        {
            BalanceModel balance = null;

            using (Context.AppContext dbContext = new Context.AppContext())
            {
                balance = dbContext.Balance.FirstOrDefault();
            }

            return balance;
        }

        internal void UpdateCost(CostModel updateCost)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                CostModel currentCost = dbContext.Costs.FirstOrDefault(c => c.Id == updateCost.Id);
                dbContext.Entry(currentCost).CurrentValues.SetValues(updateCost);
                dbContext.SaveChanges();
            }
        }

        internal void UpdateCategory(CategoryModel updateCategory)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                CategoryModel currentCategory = dbContext.Categories.FirstOrDefault(c => c.Id == updateCategory.Id);
                dbContext.Entry(currentCategory).CurrentValues.SetValues(updateCategory);
                dbContext.SaveChanges();
            }
        }

        internal void UpdateBalance(BalanceModel updateBalance)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                BalanceModel currentBalance = dbContext.Balance.FirstOrDefault(b => b.Id == updateBalance.Id);
                dbContext.Entry(currentBalance).CurrentValues.SetValues(updateBalance);
                dbContext.SaveChanges();
            }
        }

        public static DbWorker Current = new DbWorker();
    }
}
