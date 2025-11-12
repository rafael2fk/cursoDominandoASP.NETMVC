var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();  // para ter o controller funcionando

var app = builder.Build();

app.UseRouting();                          // para usar routes

app.MapControllerRoute(                    // mapeando a estrutura da controller
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
