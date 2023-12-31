﻿using System.Data.Entity;
using VipeBus.Application.Entities.Buses;
using VipeBus.Application.Entities.Cities;
using VipeBus.Application.Entities.Drivers;
using VipeBus.Application.Entities.Routes;
using VipeBus.Application.Entities.Trips;
using VipeBus.Application.Entities.Users;

namespace VipeBus.Core
{
    public class VipeBusContext : DbContext
    {
        public VipeBusContext() : base("DbVipeBus")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>()
                .HasRequired(c => c.DeparturePoint)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasRequired(c => c.DestinationPoint)
                .WithMany()
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Route>()
                .HasRequired(c => c.Driver)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Bus> Buses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Driver> Drivers { get; set; }
    }
}
