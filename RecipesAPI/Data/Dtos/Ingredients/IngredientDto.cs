using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Dtos.Ingredients
{
    public record IngredientDto(int Id, string Name, string Description);
}
