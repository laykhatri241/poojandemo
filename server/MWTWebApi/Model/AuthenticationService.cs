using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWTWebApi.Model
{
    public static  class AuthenticationService
    {
        public static void AuthenticateConfig(this IServiceCollection services )
        {
            var key = Guid.NewGuid().ToString().Replace("-", "");
            services.AddAuthentication(jwt =>
            {
                jwt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                jwt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>{
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<IAuthentication>(new Authentication(key));
        }
    }
}
