using piggy_bank_uwp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace piggy_bank_uwp.Workers
{
    public sealed class DbWorker
    {
        private const int COSTS_COUNT = 5;

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

            costs.Reverse();

            return costs;
        }

        /// <summary>
        /// Return a cost begin with count
        /// </summary>
        /// <param name="count">Count is a start index</param>
        /// <returns></returns>
        internal IEnumerable<CostModel> GetCosts(int count)
        {
            List<CostModel> costs = null;

            using (Context.AppContext dbContext = new Context.AppContext())
            {
                costs = new List<CostModel>(dbContext.Costs.Skip(count).Take(COSTS_COUNT));
            }

            return costs;
        }

        internal CostModel GetCost(string id)
        {
            CostModel result = null;

            using (Context.AppContext dbContext = new Context.AppContext())
            {
                result = dbContext.Costs.FirstOrDefault(c => c.Id == id);
            }

            return result;
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
