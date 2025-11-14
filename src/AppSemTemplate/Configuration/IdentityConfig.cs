using AppSemTemplate.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppSemTemplate.Configuration
{
    public static  class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddRoles<IdentityRole>()                                 // sempre 1
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluirPermanentemente", policy =>
                        policy.RequireRole("Admin"));

                options.AddPolicy("VerProdutos", policy =>
                    policy.RequireClaim("Produtos", "VI"));
            });

            return builder;
        }
    }
}
