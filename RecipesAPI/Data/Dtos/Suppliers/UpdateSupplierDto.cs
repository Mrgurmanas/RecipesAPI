using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesAPI.Data.Dtos.Suppliers
{
    public record UpdateSupplierDto([Required] string Name,[Required] string Website);
}
