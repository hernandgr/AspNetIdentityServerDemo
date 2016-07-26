using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;

namespace WebAuthDemo.AuthServer.Managers
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "testuser",
                    Username = "testuser",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim (Constants.ClaimTypes.Name, "Test user")
                    }
                }
            };
        }

        public List<Scope> GetScopes()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read user data"
                }
            };
        }

        public List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "webapp",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "My web application",
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.OfflineAccess,
                        "read"
                    },
                    Enabled = true
                },
                new Client
                {
                    ClientId = "webapp_implicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "My web application",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:43421",
                        "http://localhost:43421/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:43421"
                    },
                    Enabled = true
                },
                new Client
                {
                    ClientId = "webapp_code",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "My web application",
                    Flow = Flows.Hybrid,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.OfflineAccess,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:43421/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:43421"
                    },
                    Enabled = true
                }
            };
        }
    }
}