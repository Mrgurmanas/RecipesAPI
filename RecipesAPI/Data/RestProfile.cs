using AutoMapper;
using RecipesAPI.Data.Dtos.Auth;
using RecipesAPI.Data.Dtos.Ingredients;
using RecipesAPI.Data.Dtos.Recipes;
using RecipesAPI.Data.Dtos.Suppliers;
using RecipesAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data
{
    public class RestProfile : Profile
    {
        public RestProfile()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<UpdateRecipeDto, Recipe>();

            CreateMap<Ingredient, IngredientDto>();
            CreateMap<CreateIngredientDto, Ingredient>();
            CreateMap<UpdateIngredientDto, Ingredient>();

            CreateMap<Supplier, SupplierDto>();
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();
            CreateMap<RestUser, UserDto>();
        } 
    }
}
