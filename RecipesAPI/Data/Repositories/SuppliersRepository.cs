using Microsoft.EntityFrameworkCore;
using RecipesAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Repositories
{
    public interface ISuppliersRepository
    {
        Task<Supplier> GetAsync(int ingredientId, int supplierId);
        Task<List<Supplier>> GetAllAsync(int ingredientId);
        Task InsertAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task DeleteAsync(Supplier supplier);
    }

    public class SuppliersRepository : ISuppliersRepository
    {
        public readonly RestContext _restContext;

        public SuppliersRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<Supplier> GetAsync(int ingredientId, int supplierId)
        {
            return await _restContext.Suppliers.FirstOrDefaultAsync(o => o.Ingredient.Id == ingredientId && o.Id == supplierId);
        }

        public async Task<List<Supplier>> GetAllAsync(int ingredientId)
        {
            return await _restContext.Suppliers.Where(o => o.Ingredient.Id == ingredientId).ToListAsync();
        }

        public async Task InsertAsync(Supplier supplier)
        {
            _restContext.Suppliers.Add(supplier);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _restContext.Suppliers.Update(supplier);
            await _restContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Supplier supplier)
        {
            _restContext.Suppliers.Remove(supplier);
            await _restContext.SaveChangesAsync();
        }
    }
}
