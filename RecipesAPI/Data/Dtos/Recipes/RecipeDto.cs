using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Dtos.Recipes
{
    public record RecipeDto(int Id, string Name, string Description);
}
