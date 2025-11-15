using AppSemTemplate.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGlobalizationConfig()             // primeiro 
    .AddElmahConfiguration()
    .AddMvcConfiguration()
    .AddIdentityConfiguration()              //Padrao builder 
    .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
