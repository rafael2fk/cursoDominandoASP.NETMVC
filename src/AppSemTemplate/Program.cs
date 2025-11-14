using AppSemTemplate.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddMvcConfiguration()
    .AddIdentityConfiguration()              //Padrao builder 
    .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
