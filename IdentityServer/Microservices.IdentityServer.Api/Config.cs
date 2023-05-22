// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Microservices.IdentityServer.Api
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] // oluşturduk
        {
            new ApiResource("resource_beverages"){Scopes={"beverages_fullpermisson"}},    

            new ApiResource("resource_photos"){Scopes={"photos_fullpermisson"}},

            new ApiResource("resource_basket"){Scopes={"basket_fullpermisson"}},

            new ApiResource("resource_shopping_discount"){Scopes={"shopping_discount_fullpermisson"}},

            new ApiResource("resource_product_orders"){Scopes={"product_orders_fullpermisson"}},

            new ApiResource("resource_payment"){Scopes={"payments_fullpermisson"}},

            new ApiResource("resource_gateway"){Scopes={"gateway_fullpermisson"}},

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       //Kullanıcının erşebilecekleri
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),                    
                       new IdentityResource()
                       {
                           Name = "roles",
                           DisplayName = "Roles",
                           Description = "Kullanıcı rolleri",
                           UserClaims = new []{"role"}
                        }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("beverages_fullpermisson","Beverages API için full erişim"),
                
                new ApiScope("photos_fullpermisson","Photo Stock API için full erişim"),

                new ApiScope("basket_fullpermisson","Basket API için full erişim"),

                new ApiScope("shopping_discount_fullpermisson","ShoppinDiscounts API için full erişim"),

                new ApiScope("product_orders_fullpermisson","ProductOrder API için full erişim"),

                new ApiScope("payments_fullpermisson","PaymentTransaction API için full erişim"),

                new ApiScope("gateway_fullpermisson","Gateway API için full erişim"),

                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client  // 3 parametreli token alma-not user
                {
                     ClientName = "Asp.Net Core Mvc",
                     ClientId = "WebMvcClient",                   
                     ClientSecrets ={new Secret ("secret".Sha256())},                     
                     AllowedGrantTypes = GrantTypes.ClientCredentials,
                     AllowedScopes = { 
                        "beverages_fullpermisson",                         
                        "photos_fullpermisson",
                        "gateway_fullpermisson",
                        IdentityServerConstants.LocalApi.ScopeName}
                },

                new Client // 5 parametreli token alma 
                {
                     ClientName = "Asp.Net Core Mvc",
                     ClientId = "WebMvcClientForUser",
                     AllowOfflineAccess = true,
                     ClientSecrets ={new Secret ("secret".Sha256())},
                     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                     //Kullanıcının erşebilecekleri
                     AllowedScopes = {
                        "basket_fullpermisson",
                        "shopping_discount_fullpermisson",
                        "product_orders_fullpermisson",
                        "payments_fullpermisson",
                        "gateway_fullpermisson",
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OfflineAccess,
                         IdentityServerConstants.LocalApi.ScopeName,"roles", //refresh token
                      },
                     AccessTokenLifetime = 1*60*60,  //access token ömrü
                     RefreshTokenExpiration = TokenExpiration.Absolute,
                     AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,//refresh token ömrü
                     RefreshTokenUsage = TokenUsage.ReUse  //refresh token ömrü bitince yenileme
                }
            };
    }
}