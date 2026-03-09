using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.FormGeneration.Utils;
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

    protected virtual string GetName()
    {
        return ControllerNameParser.GetControllerName(GetType());
    }

    protected virtual Type? GetFormType()
    {
        string type = HttpContext.Request.Path.ToString();
        return FormMapper.GetFormType(this.GetType(), type);
    }
}