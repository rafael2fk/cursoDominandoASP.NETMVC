using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;

namespace AppSemTemplate.Configuration
{
    public static  class LoggingConfig
    {
        public static WebApplicationBuilder AddElmahConfiguration(this WebApplicationBuilder builder)
        {
            //builder.Services.AddElmahIo(options =>
            //{                                                                    // metodo 1
            //    options.ApiKey = "35207ce1401b482a8ec804d144dbcab9";
            //    options.LogId = new Guid("6aea79a7-3685-4d20-ba0b-795db8b57328");
            //});

            builder.Services.Configure<ElmahIoOptions>(builder.Configuration.GetSection("ElmahIo")); // metodo 2
            builder.Services.AddElmahIo();

            builder.Logging.Services.Configure<ElmahIoProviderOptions>(builder.Configuration.GetSection("ElmahIO")); // metodo 3
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddElmahIo();

            builder.Logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);

            return builder;
        }
    }
}
