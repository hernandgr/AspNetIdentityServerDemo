using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using WebAuthDemo.Data.Repositories;

namespace WebAuthDemo.AuthServer.Services
{
    public class DemoUserService : UserServiceBase
    {
        private readonly UserRepository _userRepository;

        public DemoUserService()
        {
            _userRepository = new UserRepository();
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = _userRepository.Get(context.UserName, context.Password);

            if (user == null)
            {
                context.AuthenticateResult = new AuthenticateResult("User/password is wrong");
                return null;
            }

            context.AuthenticateResult = new AuthenticateResult(context.UserName, context.UserName);

            return base.AuthenticateLocalAsync(context);
        }
    }
}