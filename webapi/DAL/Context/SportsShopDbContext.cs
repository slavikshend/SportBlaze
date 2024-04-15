﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

namespace webapi.DAL.Context
{
    public class SportsShopDbContext : DbContext
    {
        public SportsShopDbContext(DbContextOptions<SportsShopDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<LogLine> LogLines { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Післясплата" },
                new PaymentMethod { Id = 2, Name = "Оплата онлайн" }
            );

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Нове замовлення" },
                new OrderStatus { Id = 2, Name = "Підтверджено" },
                new OrderStatus { Id = 3, Name = "Скасовано клієнтом" },
                new OrderStatus { Id = 4, Name = "Скасовано магазином" },
                new OrderStatus { Id = 5, Name = "У дорозі" },
                new OrderStatus { Id = 6, Name = "Діставлено" },
                new OrderStatus { Id = 7, Name = "Прийнято клієнтом" }
            );

            modelBuilder.Entity<DeliveryMethod>().HasData(
                new DeliveryMethod { Id = 1, Name = "Нова Пошта" },
                new DeliveryMethod { Id = 2, Name = "Укрпошта" },
                new DeliveryMethod { Id = 3, Name = "Justin" }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "RegisteredUser" },
                new Role { Id = 3, Name = "UnregisteredUser" }

    );
            modelBuilder.Entity<User>().HasData(
        new User
        {
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "User",
            HashedPassword = "ed647ee632f13df6c65f9e47929f13cfc35069bbaa70e50c157bac04575c4e37",
            RoleId = 1,
            Salt = "yuz1xllqhl7jcudb"
        }
    );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            LogChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void LogChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in entries)
            {
                var log = new LogLine
                {
                    TimeStamp = DateTime.Now,
                    LogLevel = "Info"
                };

                var operation = entry.State.ToString().ToLower();
                var entityType = entry.Entity.GetType().Name.ToLower();
                object entityId = null;

                if (entityType == "user")
                {
                    var emailProperty = entry.Property("Email");
                    if (emailProperty != null)
                    {
                        entityId = emailProperty.CurrentValue;
                    }
                }
                else
                {
                    entityId = entry.Property("Id").CurrentValue;
                }

                log.LogMessage = $"The {entityType} with {(entityType == "user" ? "email" : "id")} {entityId} has been {operation}";

                LogLines.Add(log);
            }
        }


    }
}