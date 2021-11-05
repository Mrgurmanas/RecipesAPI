using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Auth
{
    public interface IUserOwnedResource
    {
        string UserId { get; }
    }
}
