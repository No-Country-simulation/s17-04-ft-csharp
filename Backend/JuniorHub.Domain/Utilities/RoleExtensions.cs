using JuniorHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Domain.Utilities
{
    public static class RoleExtensions
    {
        public static string ToStringEnum(this Role role)
        {
            switch(role)
            {
                case Role.Employer:
                
                    return "Employer";
                case Role.Freelancer:
                    return "Freelancer";
                case Role.Admin:
                    return "Admin";
                default:
                    return role.ToString();
            }
        }
    }
}
