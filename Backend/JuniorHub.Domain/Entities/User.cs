using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        //es null, ya que no es obligatorio que tenga una foto cuando se registre.
        public string? MediaUrl { get; set; }
    }
}
