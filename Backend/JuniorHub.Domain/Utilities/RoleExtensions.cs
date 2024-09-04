using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Utilities;

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
