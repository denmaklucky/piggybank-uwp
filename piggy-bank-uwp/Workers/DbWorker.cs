using piggy_bank_uwp.Model;
using System;
using System.Collections.Generic;

namespace piggy_bank_uwp.Workers
{
    public sealed class DbWorker
    {
        private DbWorker() {}

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

        internal void UpdateCost(CostModel model)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Entry(model).CurrentValues.SetValues(model);
            }
        }

        internal void UpdateCategory(CategoryModel model)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Entry(model).CurrentValues.SetValues(model);
            }
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

        public static DbWorker Current = new DbWorker();
    }
}
