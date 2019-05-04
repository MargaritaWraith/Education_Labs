using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Education.WEB.Infrastructure.Authorization
{
    internal class RolesPolicy : AuthorizationPolicy
    {
        public RolesPolicy(params string[] RoleNames) : base(new []{ new RolesAuthorizationRequirement(RoleNames) }, new string[0]) { }
    }
}