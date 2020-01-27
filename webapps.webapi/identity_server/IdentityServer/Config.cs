// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityServer
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("hotelsapi", "Hotels API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "hotelsweb",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("hotelswebsecret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"hotelsapi"}
                }
            };
    }
}
