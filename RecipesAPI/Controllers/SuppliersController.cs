using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Auth.Model;
using RecipesAPI.Data.Dtos.Suppliers;
using RecipesAPI.Data.Entities;
using RecipesAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("api/recipes/{recipeId}/ingredients/{ingredientId}/suppliers")]
    [Produces("application/json")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly IAuthorizationService _authorizationService;

        public SuppliersController(ISuppliersRepository suppliersRepository, IMapper mapper, IRecipesRepository recipesRepository, IIngredientsRepository ingredientsRepository, IAuthorizationService authorizationService)
        {
            _suppliersRepository = suppliersRepository;
            _mapper = mapper;
            _ingredientsRepository = ingredientsRepository;
            _recipesRepository = recipesRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAllAsync(int recipeId, int ingredientId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }
            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with id '{ingredientId}' not found.");
            }
            var ingredients = await _suppliersRepository.GetAllAsync(ingredientId);
            return Ok(ingredients.Select(o => _mapper.Map<SupplierDto>(o)));
        }

        [HttpGet("{supplierId}")]
        public async Task<ActionResult<SupplierDto>> GetAsync(int recipeId, int ingredientId, int supplierId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }
            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with id '{ingredientId}' not found.");
            }
            var supplier = await _suppliersRepository.GetAsync(ingredientId, supplierId);
            if (supplier == null)
            {
                return NotFound($"Supplier with id '{supplierId}' not found.");
            }

            return Ok(_mapper.Map<SupplierDto>(supplier));
        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<SupplierDto>> PostAsync(int recipeId, int ingredientId, CreateSupplierDto supplierDto)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }

            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with id '{ingredientId}' not found.");
            }

            var supplier = _mapper.Map<Supplier>(supplierDto);
            supplier.IngredientId = ingredientId;

            await _suppliersRepository.InsertAsync(supplier);

            //201
            //Created supplier
            return Created($"/api/recipes/{recipeId}/ingredients/{ingredient.Id}/suppliers/{supplier.Id}", _mapper.Map<SupplierDto>(supplier));
        }

        [HttpPut("{supplierId}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<SupplierDto>> PutAsync(int recipeId, int ingredientId, int supplierId, UpdateSupplierDto supplierDto)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }

            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with id '{ingredientId}' not found.");
            }

            var oldSupplier = await _suppliersRepository.GetAsync(ingredientId, supplierId);
            if (oldSupplier == null)
            {
                return NotFound($"Supplier with id '{supplierId}' not found.");
            }

            _mapper.Map(supplierDto, oldSupplier);

            await _suppliersRepository.UpdateAsync(oldSupplier);

            return Ok(_mapper.Map<SupplierDto>(oldSupplier));
        }

        [HttpDelete("{supplierId}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int recipeId, int ingredientId, int supplierId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }

            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with id '{ingredientId}' not found.");
            }
            var supplier = await _suppliersRepository.GetAsync(ingredientId, supplierId);
            if (supplier == null)
            {
                return NotFound($"Supplier with id '{supplierId}' not found.");
            }

            await _suppliersRepository.DeleteAsync(supplier);

            //204
            return NoContent();
        }
    }
}
