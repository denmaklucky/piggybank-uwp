using piggy_bank_uwp.Model;

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

        public void AddCost(TagModel tag)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Tags.Add(tag);
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

        public void RemoveTag(TagModel tag)
        {
            using (Context.AppContext dbContext = new Context.AppContext())
            {
                dbContext.Tags.Remove(tag);
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

        public static DbWorker Current = new DbWorker();
    }
}
