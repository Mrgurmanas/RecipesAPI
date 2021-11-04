using Microsoft.EntityFrameworkCore;
using RecipesAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Repositories
{
    public interface IIngredientsRepository
    {
        Task<Ingredient> GetAsync(int recipeId, int ingredientId);
        Task<List<Ingredient>> GetAllAsync(int recipeId);
        Task InsertAsync(Ingredient ingredient);
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(Ingredient ingredient);
    }

    public class IngredientsRepository : IIngredientsRepository
    {
        public readonly RestContext _restContext;

        public IngredientsRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<Ingredient> GetAsync(int recipeId, int ingredientId)
        {
           return await _restContext.Ingredients.FirstOrDefaultAsync(o => o.Recipe.Id == recipeId && o.Id == ingredientId);
        }

        public async Task<List<Ingredient>> GetAllAsync(int recipeId)
        {
            return await _restContext.Ingredients.Where(o => o.Recipe.Id == recipeId).ToListAsync();
        }

        public async Task InsertAsync(Ingredient ingredient)
        {
            _restContext.Ingredients.Add(ingredient);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            _restContext.Ingredients.Update(ingredient);
            await _restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ingredient ingredient)
        {
            _restContext.Ingredients.Remove(ingredient);
            await _restContext.SaveChangesAsync();
        }
    }
}
