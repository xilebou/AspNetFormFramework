using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controller;

public class TestController: Microsoft.AspNetCore.Mvc.Controller, IFormController
{
    public IActionResult Form()
    {
        string type = HttpContext.Request.Path.ToString();
        FormInfo viewModel = new FormInfoFactory().CreateForm(
            FormMapper<TestController>.GetFormType(typeof(TestController), type),
            "Test",
            (Request.Scheme + "://" + Request.Host)
        );

        return View(model: viewModel);
    }
}