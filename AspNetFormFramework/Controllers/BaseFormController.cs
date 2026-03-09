using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.FormGeneration.Utils;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.Controllers;

public abstract class  BaseFormController: Controller, IFormController
{
    /// <summary>
    /// Basic implementation of GET route for forms.
    /// It will return the model specified in the request url return by <see cref="GetFormType"/>
    /// </summary>
    /// <returns></returns>
    public IActionResult Form()
    {
        FormInfo viewModel = new FormInfoFactory().CreateForm(
            GetFormType(),
            GetName(),
            (Request.Scheme + "://" + Request.Host)
        );

        return View(model: viewModel);
    }

    /// <summary>
    /// Calculates the name of the controller
    /// </summary>
    /// <returns>The name of the controller</returns>
    protected virtual string GetName()
    {
        return ControllerNameParser.GetControllerName(GetType());
    }

    /// <summary>
    /// Parses the form type from the Request URL.
    /// </summary>
    /// <returns>The form type</returns>
    protected virtual Type? GetFormType()
    {
        string type = HttpContext.Request.Path.ToString();
        return FormMapper.GetFormType(this.GetType(), type);
    }
}