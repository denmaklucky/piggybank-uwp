﻿using Microsoft.EntityFrameworkCore;
using piggy_bank_uwp.Model;

namespace piggy_bank_uwp.Context
{
    public sealed class AppContext : DbContext
    {
        public AppContext()
        {
            //TODO:find out
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Costs.db");
        }

        public DbSet<CostModel> Costs { get; set; }

        public DbSet<TagModel> Tags { get; set; }
    }
}
