using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Dtos.Auth
{
    public record RegisterUserDto([Required] string UserName, [EmailAddress][Required] string Email, [Required] string Password);
}
