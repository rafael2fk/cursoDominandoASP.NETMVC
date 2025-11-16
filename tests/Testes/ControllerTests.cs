using AppSemTemplate.Controllers;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

namespace Testes
{
    public class ControllerTests
    {
        [Fact]  // fato para comprovar
        public void TesteController_Index_Sucesso()
        {
            //padrao AAA
            // Arrange 
            var controller = new TesteController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}