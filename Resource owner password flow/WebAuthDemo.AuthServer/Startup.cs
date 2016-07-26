using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
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
            var memoryManager = new InMemoryManager();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(memoryManager.GetUsers())
                .UseInMemoryScopes(memoryManager.GetScopes())
                .UseInMemoryClients(memoryManager.GetClients());

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
    }
}