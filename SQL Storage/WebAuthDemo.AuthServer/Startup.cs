using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebAuthDemo.AuthServer.Managers;

[assembly: OwinStartup(typeof(WebAuthDemo.AuthServer.Startup))]

namespace WebAuthDemo.AuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var efOptions = new EntityFrameworkServiceOptions
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["WebAuthDemo"].ConnectionString
            };

            var memoryManager = new InMemoryManager();
            SetupClients(memoryManager.GetClients(), efOptions);
            SetupScopes(memoryManager.GetScopes(), efOptions);

            var factory = new IdentityServerServiceFactory();
            factory.RegisterConfigurationServices(efOptions);
            factory.RegisterOperationalServices(efOptions);
            factory.UserService = new Registration<IUserService>();

            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
            var certificatePassword = ConfigurationManager.AppSettings["SigningCertificatePassword"];
            
            var options = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(certificate, certificatePassword),
                RequireSsl = false,
                Factory = factory
            };
            app.UseIdentityServer(options);
        }

        public void SetupClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var context = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (context.Clients.Any())
                {
                    return;
                }

                foreach (var client in clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }
        }

        public void SetupScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var context =
                new ScopeConfigurationDbContext(options.ConnectionString,
                                                options.Schema))
            {
                if (context.Scopes.Any()) return;

                foreach (var scope in scopes)
                {
                    context.Scopes.Add(scope.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}