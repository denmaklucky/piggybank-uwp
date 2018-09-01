using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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

        public void RemoveCategory(CategoryModel category)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
        }

        public void RemoveBalance(BalanceModel balance)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Balance.Remove(balance);
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
            Context.AppContext dbContext = new Context.AppContext();
            try
            {
                costs = dbContext.Costs.ToList();
            }
            catch (SqliteException ex)
            {
                dbContext.Database.ExecuteSqlCommand("alter table Costs add BalanceId text null;");
                dbContext.Database.ExecuteSqlCommand("alter table Balance add Name text null;");
                dbContext.Database.ExecuteSqlCommand("update Balance set Name = 'My first account'");

                var firstBalance = dbContext.Balance.First();
                 dbContext.Database.ExecuteSqlCommand($"update Costs set BalanceId={firstBalance.Id};");

                costs = dbContext.Costs.ToList();
            }
            finally
            {
                if (dbContext != null)
                    dbContext.Dispose();
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
                var allCost = new List<CostModel>(dbContext.Costs);
                allCost.Reverse();
                costs = new List<CostModel>(allCost.Skip(count).Take(COSTS_COUNT));
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

        public BalanceModel GetFirstBalance()
        {
            BalanceModel balance = null;
            Context.AppContext dbContext = new Context.AppContext();
            try
            {
                balance = dbContext.Balance.FirstOrDefault();
            }
            catch (SqliteException ex)
            {
                dbContext.Database.ExecuteSqlCommand("alter table Balance add Name text null;");
                balance = dbContext.Balance.FirstOrDefault();
            }
            finally
            {
                if (dbContext != null)
                    dbContext.Dispose();
            }

            return balance;
        }

        public List<BalanceModel> GetBalances()
        {
            List<BalanceModel> balances = null;
            Context.AppContext dbContext = new Context.AppContext();
            try
            {
                balances = dbContext.Balance.ToList();
            }
            catch (SqliteException ex)
            {
                dbContext.Database.ExecuteSqlCommand("alter table Balance add Name text null;");
                dbContext.Database.ExecuteSqlCommand("update Balance set Name = 'My first account'");
            }
            finally
            {
                if (dbContext != null)
                    dbContext.Dispose();
            }

            return balances;
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
