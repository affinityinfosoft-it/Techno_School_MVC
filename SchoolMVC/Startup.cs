using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Owin;
using System.Text;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
[assembly: OwinStartupAttribute(typeof(SchoolMVC.Startup))]
namespace SchoolMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = ConfigurationManager.AppSettings["JWT_issuer"], //some string, normally web url,  
                        ValidAudience = ConfigurationManager.AppSettings["JWT_issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT_key"]))
                    }
                });
        }
    }
}
