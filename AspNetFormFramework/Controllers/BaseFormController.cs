using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.Controllers;

public abstract class  BaseFormController: Controller, IFormController
{
    public IActionResult Form()
    {
        FormInfo viewModel = new FormInfoFactory().CreateForm(
            GetFormType(),
            GetName(),
            (Request.Scheme + "://" + Request.Host)
        );

        return View(model: viewModel);
    }

    protected abstract string GetName();
    protected abstract Type? GetFormType();
}