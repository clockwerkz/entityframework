﻿using NinjaDomain.Classes;
using System.Data.Entity;
using System.Linq;
using NinjaDomain.Classes.Interfaces;

namespace NinjaDomain.DataModel
{
    public class NinjaContext : DbContext
    {
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Types()
                .Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
