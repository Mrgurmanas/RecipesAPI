using Microsoft.EntityFrameworkCore;
using RecipesAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Repositories
{
    public interface IRecipesRepository
    {
        Task CreateAsync(Recipe recipe);
        Task DeleteAsync(Recipe recipe);
        Task<Recipe> GetAsync(int id);
        Task<List<Recipe>> GetAllAsync();
        Task PutAsync(Recipe recipe);
    }

    public class RecipesRepository : IRecipesRepository
    {
        public readonly RestContext _restContext;

        public RecipesRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await _restContext.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetAsync(int recipeId)
        {
            return await _restContext.Recipes.FirstOrDefaultAsync(o => o.Id == recipeId);
        }

        public async Task CreateAsync(Recipe recipe)
        {
            _restContext.Recipes.Add(recipe);
            await _restContext.SaveChangesAsync();
        }

        public async Task PutAsync(Recipe recipe)
        {
            _restContext.Recipes.Update(recipe);
            await _restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Recipe recipe)
        {
            _restContext.Recipes.Remove(recipe);
            await _restContext.SaveChangesAsync();
        }
    }
}
