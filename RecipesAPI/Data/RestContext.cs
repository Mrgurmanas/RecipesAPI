using Microsoft.EntityFrameworkCore;
using RecipesAPI.Data.Entities;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data
{
    public class RestContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public RestContext(DbContextOptions<RestContext> options): base(options)
        {
        }
    }
}
