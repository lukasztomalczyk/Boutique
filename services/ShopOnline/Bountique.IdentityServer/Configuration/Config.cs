using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Stores;

namespace Boutique.Infrastructure.IdentityServer
{
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResource { Name = "office", DisplayName = "cos"},
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "api",
                   // ClientName = "Boutique Shop API",
                    ClientSecrets =
                    {
                        new Secret("gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ".Sha256())
                    },
                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"api"}
                    // RedirectUris = { ""};
                },
            };
        }
    }
}
