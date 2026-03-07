using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.Controllers;

abstract public class  BaseFormController: Controller, IFormController
{
    public IActionResult Form()
    {
        string type = HttpContext.Request.Path.ToString();
        FormInfo viewModel = new FormInfoFactory().CreateForm(
            FormMapper<HomeController>.GetFormType(typeof(HomeController), type),
            GetName(),
            (Request.Scheme + "://" + Request.Host)
        );

        return View(model: viewModel);
    }

    protected abstract string GetName();
}