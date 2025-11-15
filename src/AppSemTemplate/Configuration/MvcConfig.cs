using AppSemTemplate.Data;
using AppSemTemplate.Extensions;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppSemTemplate.Configuration
{
    public static class MvcConfig
    {
        public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                 .SetBasePath(builder.Environment.ContentRootPath)             //add suporte personalizado nesse padrão
                 .AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                 .AddEnvironmentVariables()
                 .AddUserSecrets(Assembly.GetExecutingAssembly(), true);         // add usersecrets

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());           // add para todo mundo 
                options.Filters.Add(typeof(FiltroAuditoria));

                MvcOptionsConfig.ConfigurarMensagensDeModelBinding(options.ModelBindingMessageProvider);        //puxando a mvcOptions
            });

            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");      // Add suporte a mudan�a de conven��o da rota das areas.
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            builder.Services.AddDbContext<AppDbContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");
            });

            builder.Services.Configure<ApiConfiguration>(
                builder.Configuration.GetSection(ApiConfiguration.ConfigName));

            return builder;
        }

        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();         //página de erro detalhada
            }
            else
            {
                app.UseExceptionHandler("/erro/500");              //redirecionamento
                app.UseStatusCodePagesWithRedirects("/erro/{0}");  //com base no status code ele vai redrc 
                app.UseHsts();
            }

            app.UseGlobalizationConfig();                           // culturas

            app.UseElmahIo();                                      //após o middle de erro
            app.UseElmahIoExtensionsLogging();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Rota de areas especializadas
            app.MapAreaControllerRoute("AreaProtudots", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
            app.MapAreaControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Gestao}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var singService = services.GetRequiredService<IOperacaoSingleton>();

                Console.WriteLine("Direto da Porgram.cs" + singService.OperacaoId);
            }

            return app;
        }
    }
    
}
