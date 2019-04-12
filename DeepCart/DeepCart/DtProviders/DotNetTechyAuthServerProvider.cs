using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using DeepCart.Models;
using System;

namespace DeepCart.DtProvider
{
    // This tutorial is by DotNet Techy YouTube Channel
    // For more info about channel You can visit this link
    //  https://www.youtube.com/c/dotnettechy

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DotNetTechyAuthServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var db = new DeepCartContext())
            {
                if (db != null)
                {
                    var user = db.Registrations.ToList();
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().UserName))
                        {
                            var currentUser = user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault();
                            identity.AddClaim(new Claim("Role", currentUser.Role));
                            identity.AddClaim(new Claim("Id",Convert.ToString( currentUser.Id)));
                            identity.AddClaim(new Claim("UserName", Convert.ToString(currentUser.UserName)));
                            identity.AddClaim(new Claim("Phone", Convert.ToString(currentUser.Phone)));
                            identity.AddClaim(new Claim("Email", Convert.ToString(currentUser.Email)));


                            var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {
                                    "DisplayName", context.UserName
                                },
                                {
                                     "Role", currentUser.Role
                                }
                             });

                            var ticket = new AuthenticationTicket(identity, props);
                            context.Validated(ticket);
                        }
                        else
                        {
                            context.SetError("invalid_grant", "Provided username and password is not matching, Please retry.");
                            context.Rejected();
                        }
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is not matching, Please retry.");
                    context.Rejected();
                }
                return;
            }
        }

    }
}