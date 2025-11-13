using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Controllers
{
    [Route("teste-di")]
    public class DiLifecycleController : Controller
    {
        public IOperacao Operacao { get; set; }

        public DiLifecycleController(IOperacao operacao)
        {
            Operacao = operacao;
        }
        public IActionResult Index()
        {
            var teste = Operacao;

            return View();
        }
    }
}
