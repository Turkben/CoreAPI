using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Libary.Configuration;
using Shared.Libary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Libary.Extensions
{
    public static class CustomTokenAuthExtension
    {
        public static void AddCustomTokenAuth(this IServiceCollection services,CustomTokenOption tokenOptions)
        {
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
     {
         //var tokenOptions = services.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();
         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidIssuer = tokenOptions.Issuer,
             ValidAudience = tokenOptions.Audiences[0],
             IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
             ValidateIssuerSigningKey = true,
             ValidateAudience = true,
             ValidateIssuer = true,
             ClockSkew = TimeSpan.Zero
         };
     });
        }
    }
}
