using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;

namespace WebAuthDemo.AuthServer.Services
{
    public class DemoUserService : UserServiceBase
    {
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            return base.AuthenticateLocalAsync(context);
        }
    }
}