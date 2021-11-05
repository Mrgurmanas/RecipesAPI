using RecipesAPI.Auth;
using RecipesAPI.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Entities
{
    public class Recipe : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }

        [Required]
        public string UserId { get; set; }
        public RestUser User { get; set; }
    }
}
