using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Data.Dtos.Recipes;
using RecipesAPI.Data.Entities;
using RecipesAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    [Produces("application/json")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IMapper _mapper;

        public RecipesController(IRecipesRepository recipesRepository, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeDto>> GetAll()
        {
            return (await _recipesRepository.GetAllAsync()).Select(o => _mapper.Map<RecipeDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> Get(int id)
        {
            var recipe = await _recipesRepository.GetAsync(id);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{id}' not found.");
            }

            return Ok(_mapper.Map<RecipeDto>(recipe));
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Post(CreateRecipeDto recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            await _recipesRepository.CreateAsync(recipe);

            //201
            //Created recipe
            return Created($"/api/recipes/{recipe.Id}", _mapper.Map<RecipeDto>(recipe));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeDto>> Put(int id, UpdateRecipeDto recipeDto)
        {
            var recipe = await _recipesRepository.GetAsync(id);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{id}' not found.");
            }
            _mapper.Map(recipeDto, recipe);
            await _recipesRepository.PutAsync(recipe);
            return Ok(_mapper.Map<RecipeDto>(recipe));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeDto>> Delete(int id)
        {
            var recipe = await _recipesRepository.GetAsync(id);
            if (recipe == null)
            {
                return NotFound($"Recipe with id '{id}' not found.");
            }

            await _recipesRepository.DeleteAsync(recipe);

            //204
            return NoContent();
        }
    }
}
