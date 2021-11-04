using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Data.Dtos.Ingredients;
using RecipesAPI.Data.Entities;
using RecipesAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("api/recipes/{recipeId}/ingredients")]
    [Produces("application/json")]
    public class IngredientsController: ControllerBase
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IMapper _mapper;
        private readonly IRecipesRepository _recipesRepository;

        public IngredientsController(IIngredientsRepository ingredientsRepository, IMapper mapper, IRecipesRepository recipesRepository)
        {
            _ingredientsRepository = ingredientsRepository;
            _mapper = mapper;
            _recipesRepository = recipesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAllAsync(int recipeId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }
            var recipes = await _ingredientsRepository.GetAllAsync(recipeId);
            return Ok(recipes.Select(o => _mapper.Map<IngredientDto>(o)));
        }

        [HttpGet("{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> GetAsyn(int recipeId, int ingredientId)
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

            return Ok(_mapper.Map<IngredientDto>(ingredient));
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> PostAsync(int recipeId, CreateIngredientDto ingredientDto)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }

            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            ingredient.RecipeId = recipeId;

            await _ingredientsRepository.InsertAsync(ingredient);

            //201
            //Created ingredient
            return Created($"/api/recipes/{recipeId}/ingredients/{ingredient.Id}", _mapper.Map<IngredientDto>(ingredient));
        }

        [HttpPut("{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> PutAsync(int recipeId, int ingredientId, UpdateIngredientDto ingredientDto)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{recipeId}' not found.");
            }

            var oldIngredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);
            if (oldIngredient == null)
            {
                return NotFound($"Ingredient with id '{ingredientId}' not found.");
            }

            _mapper.Map(ingredientDto, oldIngredient);

            await _ingredientsRepository.UpdateAsync(oldIngredient);

            return Ok(_mapper.Map<IngredientDto>(oldIngredient));
        }

        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult> DeleteAsync(int recipeId, int ingredientId)
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

            await _ingredientsRepository.DeleteAsync(ingredient);

            //204
            return NoContent();
        }
    }
}
