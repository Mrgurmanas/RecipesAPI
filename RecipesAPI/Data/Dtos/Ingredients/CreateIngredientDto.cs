using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Dtos.Ingredients
{
    public record CreateIngredientDto([Required] string Name, [Required] string Description);
}
