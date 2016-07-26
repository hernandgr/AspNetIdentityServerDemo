using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Thinktecture.IdentityModel.Clients;

namespace WebAuthDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RefreshAccessToken()
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            var client = new OAuth2Client(new Uri("http://localhost:39606/connect/token"), "webapp_code", "secret");
            var requestResponse = client.RequestAccessTokenRefreshToken(claimsPrincipal.FindFirst("refresh_token").Value);

            var refreshedIdentity = new ClaimsIdentity(User.Identity);
            refreshedIdentity.RemoveClaim(refreshedIdentity.FindFirst("access_token"));
            refreshedIdentity.RemoveClaim(refreshedIdentity.FindFirst("refresh_token"));

            refreshedIdentity.AddClaim(new Claim("access_token", requestResponse.AccessToken));
            refreshedIdentity.AddClaim(new Claim("refresh_token", requestResponse.RefreshToken));

            var authManager = Request.GetOwinContext().Authentication;
            authManager.AuthenticationResponseGrant =
                new AuthenticationResponseGrant(new ClaimsPrincipal(refreshedIdentity),
                    new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Private");
        }

        [Authorize]
        public async Task<ActionResult> Private()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var accessToken = claimsPrincipal.FindFirst("access_token").Value;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:41898/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync("api/products");
                var responseResult = await response.Content.ReadAsStringAsync();

                ViewData["data"] = responseResult;
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}