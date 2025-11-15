using AppSemTemplate.Configuration;
using AppSemTemplate.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AppSemTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ApiConfiguration ApiConfig;
        private readonly ILogger<HomeController> Logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IConfiguration configuration,
                              IOptions<ApiConfiguration> apiConfiguration,
                              ILogger<HomeController> logger,
                              IStringLocalizer<HomeController> localizer)
        {
            Configuration = configuration;
            ApiConfig = apiConfiguration.Value;
            Logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            Logger.LogInformation("Information");
            Logger.LogCritical("Critical");
            Logger.LogWarning("Warning");
            Logger.LogError("Error");

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var apiConfig = new ApiConfiguration();
            Configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);

            var secret = apiConfig.UserSecret;
            var user = Configuration[$"{ApiConfiguration.ConfigName}:UserKey"];    // 2  forma de fazer

            var domain = ApiConfig.Domain;

            ViewData["Message"] = _localizer["Seja bem vindo!"];

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }

        [Route("teste")]
        public IActionResult Teste()
        {
            throw new Exception("ALGO ERRADO NÃO ESTAVA CERTO!");     //estourando o exp

            return View("Index");
        }

        [Route("erro/{id:length(3,3)}")]      //tratamento de erro
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
