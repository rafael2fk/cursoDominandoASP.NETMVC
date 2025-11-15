using AppSemTemplate.Extensions;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace AppSemTemplate.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            //Injecao de Depend, Tipos de Ciclo de Vida
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddTransient<IOperacaoTransient, Operacao>();
            builder.Services.AddScoped<IOperacaoScoped, Operacao>();
            builder.Services.AddSingleton<IOperacaoSingleton, Operacao>();
            builder.Services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));

            builder.Services.AddTransient<OperacaoService>();  // nao guarda nenhum tipo de estado deles

            builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return builder;
        }
    }
}
