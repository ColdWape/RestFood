using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DishModel> Dishes { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> databaseContextOptions)
            : base(databaseContextOptions)
        {
            Database.EnsureCreated();
            if (!Dishes.Any())
            {
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Pizza",
                    Price = 20,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f1.png",
                    Type = "pizza",
                });
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Burger",
                    Price = 15,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f2.png",
                    Type = "burger",
                });
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Pizza",
                    Price = 17,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f3.png",
                    Type = "pizza",
                });
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Pasta",
                    Price = 18,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f4.png",
                    Type = "pasta",
                });
                Dishes.Add(new DishModel
                {
                    Name = "French Fries",
                    Price = 10,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f5.png",
                    Type = "fries",
                });
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Pizza",
                    Price = 15,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f6.png",
                    Type = "pizza",
                }); 
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Burger",
                    Price = 12,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f7.png",
                    Type = "burger",
                }); 
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Burger",
                    Price = 14,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f8.png",
                    Type = "burger",
                });
                Dishes.Add(new DishModel
                {
                    Name = "Delicious Pasta",
                    Price = 10,
                    Info = "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",
                    Picture = "../images/f9.png",
                    Type = "pasta",
                });







            }
            SaveChanges();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string clientRoleName = "client";

            string adminLogin = "secret";
            string adminPassword = "pass";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = clientRoleName };

            ClientModel adminUser = new ClientModel { Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<ClientModel>().HasData(new ClientModel[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }

    }
}
