using ClinicaAmanecer.Extenciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ClinicaAmanecer.Autorizacion
{
    public class JWTPermisionPolicies
    {
        public class PermissionRequirement(string permiso) : IAuthorizationRequirement
        {
            public string Permiso { get; } = permiso;
        }

        public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
        {
            protected override Task HandleRequirementAsync(
                AuthorizationHandlerContext context,
                PermissionRequirement requirement)
            {
                if (context.User.TienePermiso(requirement.Permiso))
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }

            public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
                : DefaultAuthorizationPolicyProvider(options)
            {
                public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
                {
                    var policy = await base.GetPolicyAsync(policyName);
                    if (policy is not null)
                        return policy;

                    return new AuthorizationPolicyBuilder()
                        .AddRequirements(new PermissionRequirement(policyName))
                        .Build();
                }
            }
        }
    }
}