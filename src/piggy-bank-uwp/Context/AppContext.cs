using Microsoft.EntityFrameworkCore;
using piggy_bank_uwp.Models;

namespace piggy_bank_uwp.Context
{
    public sealed class AppContext : DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Costs.db");
        }

        public DbSet<CostModel> Costs { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<BalanceModel> Balance { get; set; }
    }
}
