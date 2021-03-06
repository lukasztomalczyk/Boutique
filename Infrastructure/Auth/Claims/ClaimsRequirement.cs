﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Claims
{
    public class ClaimsRequirement : AuthorizationHandler<ClaimsRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if(context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value == "Admin")
            {
                 context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }
            
            return Task.CompletedTask;
        }
    }
}