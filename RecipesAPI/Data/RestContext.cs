using Microsoft.EntityFrameworkCore;
using RecipesAPI.Data.Entities;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RecipesAPI.Data.Dtos.Auth;

namespace RecipesAPI.Data
{
    public class RestContext : IdentityDbContext<RestUser>
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public RestContext(DbContextOptions<RestContext> options): base(options)
        {
        }

        /*
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             //var connString = System.Configuration.Get("Data:timesheet_db:ConnectionString");
             //Configuration.GetConnectionString("DBConnection")
             //ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString
             optionsBuilder.UseSqlServer(_connectionString);//Environment.GetEnvironmentVariable("DBConnection")
         }*/
    }
}
